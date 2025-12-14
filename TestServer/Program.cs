using Model;
using System.Threading.Tasks;
using ClientSide;
namespace TestServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Selecting from the API
            Console.WriteLine("Running Server");
            Console.WriteLine("\nSelecting from the API\n");
            ApiService apiService = new ApiService($"https://localhost:7062/");


            Console.WriteLine("Apps Server\n");
            AppsList apps = await apiService.GetApps();
            foreach (var app in apps)
            {
                Console.WriteLine($"App Name: {app.AppName}, App Id: {app.Id}");
            }

            Console.WriteLine("\nGroup Server\n");
            GroupList groups = await apiService.GetGroups();
            foreach (var group in groups)
            {
                Console.WriteLine($"Group Name: {group.GroupName}, Group Id: {group.Id}");
            }


            Console.WriteLine("\nGroupMember Server\n");
            GroupMembersList groupMembers = await apiService.GetGroupMembers();
            foreach (var groupMember in groupMembers)
            {
                Console.WriteLine($"GroupMember Name: {groupMember.Group.GroupName}, GroupMember Id: {groupMember.Id}");
            }

            Console.WriteLine("\ninstruments\n");
            InstrumentsList instruments = await apiService.GetInstruments();
            foreach (var instrument in instruments)
            {
                Console.WriteLine($"Instrument Name: {instrument.InstrumentName}, Instrument Id: {instrument.Id}");
            }
            Console.WriteLine("MusicalSegments Server");
            MusicalSegmentsList musicalSegments = await apiService.GetMusicalSegments();
            foreach (var musicalSegment in musicalSegments)
            {
                Console.WriteLine($"Musical Segment Name: {musicalSegment.SegmentName}, Musical Segment Id: {musicalSegment.Id}");
            }
            Console.WriteLine("\nmusicians\n");
            MusicianList musicians = await apiService.GetMusicians();
            foreach (var musician in musicians)
            {
                Console.WriteLine($"Musician Name: {musician.Username}, Musician Id: {musician.Id}");
            }
            Console.WriteLine("\nMusicianInstruments Server\n");
            MusicianInstrumentsList musicianInstruments = await apiService.GetMusicianInstruments();
            foreach (var musicianInstrument in musicianInstruments)
            {
                Console.WriteLine($"MusicianInstrument Id: {musicianInstrument.Id}");
            }
            Console.WriteLine("\nPerson server\n");
            PersonList persons = await apiService.GetPerson();
            foreach (var person in persons)
            {
                Console.WriteLine($"Person Name: {person.Username} {person.PassW}, Person Id: {person.Id}");
            }
            Console.WriteLine("\nproducer server\n");
            PreducerList producers = await apiService.GetProducers();
            foreach (var producer in producers)
            {
                Console.WriteLine($"Producer Name: {producer.Username}, Producer Id: {producer.Id}");
            }
            Console.WriteLine("\nproducerApps server\n");
            ProducerAppsList producerApps = await apiService.GetProducerApps();
            foreach (var producerApp in producerApps)
            {
                Console.WriteLine($"ProducerApp Id: {producerApp.Id}");
            }

            // inserting to the API
            Console.WriteLine("\nInserting to the API\n");
            //insert Apps- like the one above(for the select)
            //Console.WriteLine("Insert Apps Server\n");

            int AppsRows = await apiService.InsertApp(new Apps
            {
                AppName = "New App"
            });
            Console.WriteLine($"InsertApp Result (New Id): {AppsRows}\n");

            int PersonRows = await apiService.InsertPerson(new Person
            {
                Username = "New Person",
                PassW = "HDr2314Pass",
                IsActive = true
            });
            Console.WriteLine($"InsertPerson Result (New Id): {PersonRows}\n");

            int ProducerRows = await apiService.InsertProducer(new Producer
            {
                Username = "producerUser",
                PassW = "StrongPass!123",
                IsActive = true
            });
            Console.WriteLine($"InsertProducer Result (New Id): {ProducerRows}\n");
            int rowsAffected = await apiService.InsertProducerApp(new ProducerApps
            {
                Producer = producers.Last(),
                Apps = apps.Last()
            });

            Console.WriteLine($"InsertProducerApp Result (Rows Affected): {rowsAffected}\n");

            int MusicianRows = await apiService.InsertMusician(new Musician
            {
                IsActive = true,
                Username = "newmus",
                PassW = "passit4uto"
            });
            Console.WriteLine($"InsertMusician Result (New Id): {MusicianRows}\n");
            Instruments inst = new Instruments()
            {
                InstrumentName = "New Instrument"
            };
            int newInstrumentId = await apiService.InsertInstrument(inst);


            Console.WriteLine($"InsertInstrument Result (Rows Affected): {newInstrumentId}\n");

            int newSegmentId = await apiService.InsertMusicalSegment(new MusicalSegments
            {
                SegmentName = "New Segment",
                Lengthinseconds = 180,
                Musician = musicians.Last() /*new Musician { Id = MusicianRows }*/,
                Instruments = instruments.Last() /*new Instruments { Id = newInstrumentId }*/,
                Link = "http://newsegmentlink.com",
                Genre = "Pop",
                Mood = "Happy",
                Key = "C",
                Bpm = 120

            });
            Console.WriteLine($"InsertMusicalSegment Result (New Id): {newSegmentId}\n");

            int newGroupId = await apiService.InsertGroup(new Group
            {
                GroupName = "New Group",
                IsActive = true
            });
            Console.WriteLine($"InsertGroup Result (Rows Affected): {newGroupId}\n");

            groupMembers = await apiService.GetGroupMembers();
            //returns InsertGroupMember Result (Rows Affected ): GroupMembers: 512, Group:  
            //insted of InsertGroupMember Result (Rows Affected ): 1
            //int newGroupMemberId = await apiService.InsertGroupMember(new GroupMembers
            //{
            //    Id = persons.Last().Id,
            //    Group = new Group
            //    {
            //        Id = groups.Last().Id,
            //        GroupName = groups.Last().GroupName   // <-- required!
            //    },
            //    Username = persons.Last().Username,
            //    PassW = persons.Last().PassW,
            //    IsActive = persons.Last().IsActive
            //});

            //Console.WriteLine($"InsertGroupMember Result (Rows Affected): {newGroupMemberId}\n");

            // Create the GroupMembers object
            GroupMembers member = new GroupMembers
            {
                Id = persons.Last().Id,                 // Person PK
                Username = persons.Last().Username ?? "DefaultUsername",  // required
                PassW = persons.Last().PassW ?? "DefaultPassW",           // required
                IsActive = persons.Last().IsActive,
                Group = new Group
                {
                    Id = groups.Last().Id,             // Group PK
                    GroupName = groups.Last().GroupName,  // required
                    CreationDate = groups.Last().CreationDate,
                    IsActive = groups.Last().IsActive
                }
            };

            int rows = await apiService.InsertGroupMember(member);
            Console.WriteLine($"InsertGroupMember Rows Affected: {rows}");


            int newMusicianInstrumentId = await apiService.InsertMusicianInstrument(new MusicianInstruments
            {
                Musician = musicians.Last(),
                Instruments = instruments.Last()
            });
            Console.WriteLine($"InsertMusicianInstrument Result (Rows Affected): {newMusicianInstrumentId}\n");

            //Deleting to the API
            Console.WriteLine("\nDeleting to the API\n");

            persons = await apiService.GetPerson();
            int deletePersonResult = await apiService.DeletePerson(persons.Last().Id);
            Console.WriteLine($"DeletePerson Result (Rows Affected): {deletePersonResult}\n");

            producers = await apiService.GetProducers();
            int deleteProducerResult = await apiService.DeleteProducer(producers.Last().Id);
            Console.WriteLine($"DeleteProducer Result (Rows Affected): {deleteProducerResult}\n");

            musicians = await apiService.GetMusicians();
            int deleteMusicianResult = await apiService.DeleteMusician(musicians.Last().Id);
            Console.WriteLine($"DeleteMusician Result (Rows Affected): {deleteMusicianResult}\n");

            musicalSegments = await apiService.GetMusicalSegments();
            int deleteMusicalSegmentResult = await apiService.DeleteMusicalSegment(musicalSegments.Last().Id);
            Console.WriteLine($"DeleteMusicalSegment Result (Rows Affected): {deleteMusicalSegmentResult}\n");

            groups = await apiService.GetGroups();
            int deleteGroupResult = await apiService.DeleteGroup(groups.Last().Id);
            Console.WriteLine($"DeleteGroup Result (Rows Affected): {deleteGroupResult}\n");

            //groupMembers = await apiService.GetGroupMembers();
            //int deleteGroupMemberResult = await apiService.DeleteGroupMember(groupMembers.Last().Id);
            //Console.WriteLine($"DeleteGroupMember Result (Rows Affected): {deleteGroupMemberResult}\n");

            instruments = await apiService.GetInstruments();
            int deleteMusicianInstrumentResult = await apiService.DeleteMusicianInstrument(musicianInstruments.Last().Id);
            Console.WriteLine($"DeleteMusicianInstrument Result (Rows Affected): {deleteMusicianInstrumentResult}\n");

            producerApps = await apiService.GetProducerApps();
            int deleteProducerAppResult = await apiService.DeleteProducerApp(producerApps.Last().Id);
            Console.WriteLine($"DeleteProducerApp Result (Rows Affected): {deleteProducerAppResult}\n");

            //Updating to the API
            Console.WriteLine("\nUpdating to the API\n");
            // Update Musician
            musicians = await apiService.GetMusicians();
            if (musicians.Count > 0)
            {
                var musicianToUpdate = musicians.Last();
                musicianToUpdate.PassW = musicianToUpdate.PassW + "updPass";
                int updateMusicianResult = await apiService.UpdateMusician(musicianToUpdate);
                Console.WriteLine($"UpdateMusician Result (Rows Affected): {updateMusicianResult}\n");
            }

            // Update Person
            persons = await apiService.GetPerson();
            if (persons.Count > 0)
            {
                var personToUpdate = persons.Last();
                personToUpdate.PassW = personToUpdate.PassW + "updPass";
                int updatePersonResult = await apiService.UpdatePerson(personToUpdate);
                Console.WriteLine($"UpdatePerson Result (Rows Affected): {updatePersonResult}\n");
            }

            // Update Producer
            producers = await apiService.GetProducers();
            if (producers.Count > 0)
            {
                var producerToUpdate = producers.Last();
                producerToUpdate.PassW = producerToUpdate.PassW + "updPass";
                int updateProducerResult = await apiService.UpdateProducer(producerToUpdate);
                Console.WriteLine($"UpdateProducer Result (Rows Affected): {updateProducerResult}\n");
            }

            // Update ProducerApp 
            producerApps = await apiService.GetProducerApps();
            if (producerApps.Count > 0)
            {
                var pappToUpdate = producerApps.Last();
                int updateProducerAppResult = await apiService.UpdateProducerApp(pappToUpdate);
                Console.WriteLine($"UpdateProducerApp Result (Rows Affected): {updateProducerAppResult}\n");
            }

            // Update Apps
            apps = await apiService.GetApps();
            if (apps.Count > 0)
            {
                var appToUpdate = apps.Last();
                appToUpdate.AppName = /*appToUpdate.AppName + */"updApp";
                int updateAppResult = await apiService.UpdateApp(appToUpdate);
                Console.WriteLine($"UpdateApp Result (Rows Affected): {updateAppResult}\n");
            }

            // Update Group
            groups = await apiService.GetGroups();
            if (groups.Count > 0)
            {
                var groupToUpdate = groups.Last();
                groupToUpdate.GroupName = /*groupToUpdate.GroupName + */"UpdGroup";
                int updateGroupResult = await apiService.UpdateGroup(groupToUpdate);
                Console.WriteLine($"UpdateGroup Result (Rows Affected): {updateGroupResult}\n");
            }

            // Update GroupMembers-needs fix probebly in API side
            groupMembers = await apiService.GetGroupMembers();
            if (groupMembers.Count > 0)
            {
                var memberToUpdate = groupMembers.Last();

                // keep existing required fields
                //The ?? operator returns the value on its left if it’s not null. otherwise, it returns the value on its right.
                memberToUpdate.PassW ??= "existingPassword";   // keep the old password
                memberToUpdate.Group ??= new Group { Id = 1, GroupName = "Rock Band", CreationDate = DateTime.Now, IsActive = true };

                // modify what you want
                memberToUpdate.Username = "Renamed";
                memberToUpdate.IsActive = true;

                int updateResult = await apiService.UpdateGroupMember(memberToUpdate);
                Console.WriteLine($"UpdateGroupMember Result (Rows Affected): {updateResult}");
            }

            // Update Instruments
            instruments = await apiService.GetInstruments();
            if (instruments.Count > 0)
            {
                var instrumentToUpdate = instruments.Last();
                instrumentToUpdate.InstrumentName = "RenamedInst";
                int updateInstrumentResult = await apiService.UpdateInstrument(instrumentToUpdate);
                Console.WriteLine($"UpdateInstrument Result (Rows Affected): {updateInstrumentResult}\n");
            }

            // Update MusicalSegments
            musicalSegments = await apiService.GetMusicalSegments();
            if (musicalSegments.Count > 0)
            {
                var segmentToUpdate = musicalSegments.Last();
                segmentToUpdate.SegmentName = "RenamedSeg";
                int updateSegmentResult = await apiService.UpdateMusicalSegment(segmentToUpdate);
                Console.WriteLine($"UpdateMusicalSegment Result (Rows Affected): {updateSegmentResult}\n");
            }


            Console.ReadLine();


        }
    }
}
