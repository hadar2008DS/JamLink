using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientSide
{
    public interface IApiService
    {
        //Task<List<T>> GetAllAsync<T>(string endpoint);
        //Task<bool> PostAsync<T>(string endpoint, T item);
        //Task<bool> Update<T>(string endpoint, int id, T item);
        //Task<bool> DeleteAsync(string endpoint, int id);

        //add specific methods for each model

        #region Select
        //select methods
        Task<AppsList> GetApps();
        Task<GroupList> GetGroups();
        Task<GroupMembersList> GetGroupMembers();
        Task<InstrumentsList> GetInstruments();
        Task<MusicalSegmentsList> GetMusicalSegments();
        Task<MusicianList> GetMusicians();
        Task<MusicianInstrumentsList> GetMusicianInstruments();
        Task<PersonList> GetPerson();
        Task<PreducerList> GetProducers();
        Task<ProducerAppsList> GetProducerApps();

        #endregion

        #region Insert
        //insert methods
        Task<int> InsertApp(Apps app);
        Task<int> InsertGroup(Group group);
        Task<int> InsertGroupMember(GroupMembers groupMember);
        Task<int> InsertInstrument(Instruments instrument);
        Task<int> InsertMusicalSegment(MusicalSegments musicalSegment);
        Task<int> InsertMusician(Musician musician);
        Task<int> InsertMusicianInstrument(MusicianInstruments musicianInstrument);
        Task<int> InsertPerson(Person person);
        Task<int> InsertProducer(Producer producer);
        Task<int> InsertProducerApp(ProducerApps producerApp);
        #endregion

        #region Update
        //update methods
        Task<int> UpdateAppAsync(Apps app);
        Task<int> UpdateGroupAsync(Group group);
        Task<int> UpdateGroupMemberAsync(GroupMembers groupMember);
        Task<int> UpdateInstrumentAsync(Instruments instrument);
        Task<int> UpdateMusicalSegmentAsync(MusicalSegments musicalSegment);
        Task<int> UpdateMusicianAsync(Musician musician);
        Task<int> UpdateMusicianInstrumentAsync(MusicianInstruments musicianInstrument);
        Task<int> UpdatePersonAsync(Person person);
        Task<int> UpdateProducerAsync(Producer producer);
        Task<int> UpdateProducerAppAsync(ProducerApps producerApp);
        #endregion

        #region Delete
        //delete methods
        Task<int> DeleteGroup(int groupId);
        Task<int> DeleteGroupMember(int groupMemberId);
        Task<int> DeleteMusicalSegment(int segmentId);
        Task<int> DeleteMusician(int musicianId);
        Task<int> DeleteMusicianInstrument(int musicianInstrumentId);
        Task<int> DeletePerson(int personId);
        Task<int> DeleteProducer(int producerId);
        Task<int> DeleteProducerApp(int producerAppId);
        #endregion




        //static async Task Main(string[] args)
        //{
        //        var host = Host.CreateDefaultBuilder(args)
        //            .ConfigureServices((context, services) =>
        //            {
        //                services.AddHttpClient();
        //                services.AddScoped<Apiservice>(sp => {
        //                    var client = sp.GetRequiredService<HttpClient>();

        //                }

        //                );
        //                })
        //            .Build();
        //        await host.RunAsync();
        //    }
        //}
    }
}
