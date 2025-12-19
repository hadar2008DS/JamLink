using Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ClientSide
{
    public class ApiService : IApiService
    {
        private readonly HttpClient client;
        string uri= "https://3vtqpqlm-7062.euw.devtunnels.ms/";
        public ApiService(string connection)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(connection);
        }



        private async Task<T> Select<T>(string endpoint)
                where T : new() // T (T is a generic type) must have an empty (parameterless) constructor
        {
            try
            {
                // Send GET request to the server and convert the JSON response to type T
                var result = await client.GetFromJsonAsync<T>(endpoint);
                if (result is null)
                {
                    // If the server returned nothing, treat it as an error
                    // so we don't return null for a non-nullable type
                    throw new HttpRequestException($"Empty response from {endpoint}");
                }
                // Return the data we got from the server
                return result;
            }
            catch (Exception ex)
            {
                // Print the error to help with debugging
                Console.WriteLine($"Error fetching data from {endpoint}: {ex.Message}");
                // If something went wrong, try to return a default object instead of crashing
                try
                {
                    // Create a new instance of T at runtime (fallback object)
                    //A fallback is a backup value
                    //If something goes wrong, instead of crashing or returning null, we return a safe default object.
                    var fallback = Activator.CreateInstance(typeof(T)); 
                    // Check that the created object is really of type T
                    if (fallback is T typedFallback)
                        return typedFallback; // return a default or empty T object
                    throw new HttpRequestException($"Unable to create fallback instance of {typeof(T).FullName}");
                }
                catch (Exception createEx)
                {
                    // If even the fallback creation fails, throw a clear error
                    throw new HttpRequestException($"Error fetching data from {endpoint}: {ex.Message}; fallback failed: {createEx.Message}", ex);
                }
            }
        }


        // Generic method to insert any object into the API and get back an ID
        // If the server does not return an ID, the method returns -1
        private async Task<int> Insert<T>(string endpoint, T entity)
        where T : new() // T (T is a generic type) must have an empty (parameterless) constructor
        {
            int? result = null; // will store the ID if we manage to get one

            try
            {
                // Send POST request with the object as JSON
                HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, entity);
                string content = await response.Content.ReadAsStringAsync();// Read the server response as plain text

                // If the request failed (400, 500, etc.), throw an error
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Request failed: {response.StatusCode} - {content}");
                // First try: maybe the server returned just a number (ID)
                if (int.TryParse(content, out int intResult))
                    result = intResult;
                else
                {
                    try  // Second try: maybe the server returned JSON with an "id" field
                    {
                        using var doc = JsonDocument.Parse(content);
                        // Try to find a property called "id" and read it as int
                        if (doc.RootElement.TryGetProperty("id", out JsonElement idElement) 
                            && idElement.TryGetInt32(out int jsonId))
                            result = jsonId;
                    }
                    catch { /* If the response is not valid JSON, just ignore it */ }
                }
                // Log what the server sent back (useful for debugging)
                Console.WriteLine($"Server returned message: {content}");
            }
            catch (Exception ex)
            {
                // Print the full error so we know what went wrong
                Console.WriteLine($"Insert to {endpoint} failed: {ex}");

                // Throw a clear and simple error message
                // We do not attach the original exception to keep things simple 

                throw new HttpRequestException($"Error inserting data to {endpoint}: {ex.Message}");
            }

            // If we got an ID, return it
            // If not, return -1 so the caller always gets an int
            return result ?? -1;  
        }



        private async Task<int> Update<T>(string route, T item)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(item);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage resp = await client.PutAsync(route, content);
            //if (!resp.IsSuccessStatusCode)
            //    return -1;
            if (!resp.IsSuccessStatusCode)
            {
                string errorContent = await resp.Content.ReadAsStringAsync();
                Console.WriteLine("SERVER ERROR RESPONSE:\n" + errorContent);
            }

            string result = await resp.Content.ReadAsStringAsync();
            Console.WriteLine($"Server returned message: {result}");

            // need to return an int value here.
            // Try to parse the result as int, otherwise return -1.
            if (int.TryParse(result, out int intResult))
                return intResult;
            else
                return -1;
        }


        private async Task<int> Delete(string endpoint, int id)
        {
            int changedRecords = -1;

            try
            {
                //basic DELETE requst does not support body, where I hid the id,
                //so we need to create the request manually
                var request = new HttpRequestMessage(HttpMethod.Delete, endpoint)
                {
                    // Use JsonContent.Create to serialize the integer ID into the body
                    Content = JsonContent.Create(id)
                };

                HttpResponseMessage response = await client.SendAsync(request);

                // 2. Check for failure status codes
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();

                    throw new HttpRequestException(
                        $"Request failed: {response.StatusCode} - {errorContent}",
                        null,
                        response.StatusCode
                    );
                }

                // 3. Read the response string (which contains the changed records count)
                string responseContent = await response.Content.ReadAsStringAsync();

                // The server returns a verbose string (e.g., "... Records changed: 1"). 
                // We need to reliably extract the number of affected rows.

                // Find "Records changed:"
                changedRecords = int.Parse(responseContent);

            }
            catch (Exception ex)
            {
                // Centralized error logging
                Console.WriteLine($"Error deleting data via {endpoint}: {ex.Message}");
            }
            return changedRecords;
        }



        // Apps
        public Task<AppsList> GetApps() => Select<AppsList>(uri+"api/Select/AppsSelector");
        public Task<int> InsertApp(Apps app) => Insert(uri + "api/Insert/InsertApp", app);//is working on the client side
        public Task<int> UpdateApp(Apps app) => Update(uri + "api/Update/UpdateApp", app);
        public Task<int> UpdateAppAsync(Apps app) => UpdateApp(app);
       


        // Group
        public Task<GroupList> GetGroups() => Select<GroupList>(uri + "api/Select/GroupSelector");
        public Task<int> InsertGroup(Group group) => Insert(uri + "api/Insert/InsertGroup", group);
        public Task<int> UpdateGroup(Group group) => Update(uri + "api/Update/UpdateGroup", group);
        public Task<int> UpdateGroupAsync(Group group) => UpdateGroup(group);
        public Task<int> DeleteGroup(int groupId) => Delete($"api/Delete/DeleteGroup", groupId);

        // GroupMembers
        public Task<GroupMembersList> GetGroupMembers() => Select<GroupMembersList>(uri + "api/Select/GroupMembersSelector");
        public Task<int> InsertGroupMember(GroupMembers groupMember) => Insert(uri + "api/Insert/InsertGroupMember", groupMember);
        public Task<int> UpdateGroupMember(GroupMembers groupMember) => Update(uri + "api/Update/UpdateGroupMember", groupMember);
        public Task<int> UpdateGroupMemberAsync(GroupMembers groupMember) => UpdateGroupMember(groupMember);
        public Task<int> DeleteGroupMember(int groupMemberId) => Delete($"api/Delete/DeleteGroupMembers",groupMemberId);



        // Instruments
        public Task<InstrumentsList> GetInstruments() => Select<InstrumentsList>(uri + "api/Select/InstrumentsSelector");
        public Task<int> InsertInstrument(Instruments instrument) => Insert(uri + "api/Insert/InsertInstrument", instrument);
        public Task<int> UpdateInstrument(Instruments instrument) => Update(uri + "api/Update/UpdateInstrument", instrument);
        public Task<int> UpdateInstrumentAsync(Instruments instrument) => UpdateInstrument(instrument);


        // MusicalSegments
        public Task<MusicalSegmentsList> GetMusicalSegments() => Select<MusicalSegmentsList>(uri + "api/Select/MusicalSegmentsSelector");
        public Task<int> InsertMusicalSegment(MusicalSegments segment) => Insert(uri + "api/Insert/InsertMusicalSegment", segment);
        public Task<int> UpdateMusicalSegment(MusicalSegments segment) => Update(uri + "api/Update/UpdateMusicalSegment", segment);
        public Task<int> UpdateMusicalSegmentAsync(MusicalSegments musicalSegment) => UpdateMusicalSegment(musicalSegment);
        public Task<int> DeleteMusicalSegment(int segmentId) => Delete($"api/Delete/DeleteMusicalSegment",segmentId);


        // MusicianInstruments
        public Task<MusicianInstrumentsList> GetMusicianInstruments() => Select<MusicianInstrumentsList>(uri + "api/Select/MusicianInstrumentsSelector");
        public Task<int> InsertMusicianInstrument(MusicianInstruments musicianInstrument) => Insert(uri + "api/Insert/InsertMusicianInstrument", musicianInstrument);
        public Task<int> UpdateMusicianInstrument(MusicianInstruments musicianInstrument) => Update(uri + "api/Update/UpdateMusicianInstrument", musicianInstrument);
        public Task<int> UpdateMusicianInstrumentAsync(MusicianInstruments musicianInstrument) => UpdateMusicianInstrument(musicianInstrument);
        public Task<int> DeleteMusicianInstrument(int musicianInstrumentId) => Delete($"api/Delete/DeleteMusicianInstrument",musicianInstrumentId);


        // Musician
        public Task<MusicianList> GetMusicians() => Select<MusicianList>(uri + "api/Select/MusiciansSelector");
        public Task<int> InsertMusician(Musician musician) =>Insert(uri + "api/Insert/InsertMusician", musician);
        public Task<int> UpdateMusician(Musician musician) => Update(uri + "api/Update/UpdateMusician", musician);
        public Task<int> UpdateMusicianAsync(Musician musician) => UpdateMusician(musician);
        public Task<int> DeleteMusician(int musicianId) => Delete($"api/Delete/DeleteMusician",musicianId);

        // Person
        public Task<PersonList> GetPeople() => Select<PersonList>(uri + "api/Select/PersonSelector");
        public Task<PersonList> GetPerson() => Select<PersonList>(uri + "api/Select/PersonSelector");
        public Task<int> InsertPerson(Person person) => Insert(uri + "api/Insert/InsertPerson", person);//is working on client side
        public Task<int> UpdatePerson(Person person) => Update(uri + "api/Update/UpdatePerson", person);
        public Task<int> UpdatePersonAsync(Person person) => UpdatePerson(person);
        public Task<int> DeletePerson(int personId) => Delete($"api/Delete/DeletePerson",personId);


        // Producer
        public Task<PreducerList> GetProducers() => Select<PreducerList>(uri + "api/Select/ProducerSelector");
        public Task<int> InsertProducer(Producer producer) => Insert(uri + "api/Insert/InsertProducer", producer);//is working on client side
        public Task<int> UpdateProducer(Producer producer) => Update(uri + "api/Update/UpdateProducer", producer);   
        public Task<int> UpdateProducerAsync(Producer producer) => UpdateProducer(producer);
        public Task<int> DeleteProducer(int producerId) => Delete($"api/Delete/DeleteProducer",producerId);

        // ProducerApps
        public Task<ProducerAppsList> GetProducerApps() => Select<ProducerAppsList>(uri + "api/Select/ProducerAppsSelector");
        public Task<int> InsertProducerApp(ProducerApps producerApp) => Insert(uri + "api/Insert/InsertProducerApp", producerApp);
        public Task<int> UpdateProducerApp(ProducerApps producerApp) => Update(uri + "api/Update/UpdateProducerApp", producerApp);
        public Task<int> UpdateProducerAppAsync(ProducerApps producerApp) => UpdateProducerApp(producerApp);
        public Task<int> DeleteProducerApp(int producerAppId) => Delete($"api/Delete/DeleteProducerApp",producerAppId);
    }
}
