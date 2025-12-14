using Model;
using ViewModel;
namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {


            




            // Test PersonDB and PersonList
            Console.WriteLine();
            Console.WriteLine("Person:");
            Console.WriteLine();
            PersonDB pdb = new();
            PersonList pList = pdb.SelectAll();
            foreach (Person p in pList)
            {
                Console.WriteLine(p.ToString());
            }
            #region Person  Update, Insert, Delete Tests
            // Test Update Person
            Console.WriteLine();

            if (pList.Count > 0)
            {
                Person personToUpdate = pList[0];
                personToUpdate.Username = "New User Name";
                pdb.Update(personToUpdate);
                int rowsUpdated1 = pdb.SaveChanges();
                Console.WriteLine($"{rowsUpdated1} rows were updated");
            }
            //Test Insert Person
            Console.WriteLine();
            Person personInsert = new Person { Username = "Inserted User", PassW = "password", IsActive = true };
            pdb.Insert(personInsert);
            int rowsInserted = pdb.SaveChanges();
            Console.WriteLine($"{rowsInserted} rows were inserted");
            foreach (Person p in pList)
            {
                Console.WriteLine(p.ToString());
            }
            //Test Delete Person (Soft Delete)
            Console.WriteLine();
            var personToDelete = pList.Find(x => x.Username == "Inserted User");
            if (personToDelete != null)
            {
                Console.WriteLine($"Deleting Person Id:{personToDelete.Id}, Username:{personToDelete.Username}");
                pdb.Delete(personToDelete);
                int rowsDeletedPerson = pdb.SaveChanges();
                Console.WriteLine($"{rowsDeletedPerson} rows were deleted (Person soft delete).");
            }
            foreach (Person p in pList)
            {
                Console.WriteLine(p.ToString());
            }
            #endregion
            Console.WriteLine("-----------------------------");
            // Test MusicianDB and MusicianList
            Console.WriteLine();
            Console.WriteLine("Musician:");
            Console.WriteLine();
            MusicianDB mdb = new();
            MusicianList mList = mdb.SelectAll();
            foreach (Musician m in mList)
            {
                Console.WriteLine(m.ToString());
            }
            #region Musician Update, Insert, Delete Tests
            // Test Update Musician
            Console.WriteLine();

            if (mList.Count > 0)
            {
                Musician musicianToUpdate = mList[0];
                musicianToUpdate.IsActive = false;
                mdb.Update(musicianToUpdate);
                int rowsUpdated2 = mdb.SaveChanges();
                Console.WriteLine($"{rowsUpdated2} rows were updated");
            }
            //Test Insert Musician
            Console.WriteLine();
            Musician musicianInsert = new Musician { IsActive = true, Id = personInsert.Id , PassW="xxxx", Username="yyyy"};
            mdb.Insert(musicianInsert);
            int rowsInserted1 = mdb.SaveChanges();
            Console.WriteLine($"{rowsInserted1} rows were inserted");
            foreach (Musician m in mList)
            {
                Console.WriteLine(m.ToString());
            }
            //Test Delete Musician (Soft Delete)
            Console.WriteLine();
            var musicianToDelete2 = mList.LastOrDefault();
            if (musicianToDelete2 != null)
            {
                Console.WriteLine($"Soft deleting Musician Id:{musicianToDelete2.Id}");
                //musicianToDelete2.IsActive = false;
                mdb.Delete(musicianToDelete2);
                int rowsDeletedMusician = mdb.SaveChanges();
                Console.WriteLine($"{rowsDeletedMusician} rows were soft deleted (Musician soft delete).");
            }
            else
            {
                Console.WriteLine("No musicians to delete.");
            }

            #endregion
            Console.WriteLine("-----------------------------");
            // Test ProducerDB and PreducerList
            Console.WriteLine();
            Console.WriteLine("Producer:");
            Console.WriteLine();
            ProducerDB prdb = new();
            PreducerList prList = prdb.SelectAll();
            foreach (Producer pr in prList)
            {
                Console.WriteLine(pr.ToString());
            }
            #region Producer Update, Insert, Delete Tests
            // Test Update Producer
            Console.WriteLine();

            if (prList.Count > 0)
            {
                Producer producerToUpdate = prList[0];
                producerToUpdate.IsActive = false;
                prdb.Update(producerToUpdate);
                int rowsUpdated3 = prdb.SaveChanges();
                Console.WriteLine($"{rowsUpdated3} rows were updated");
            }
            //Test Insert Producer
            Console.WriteLine();
            Producer producerInsert = new Producer { IsActive = false, Id = personInsert.Id, Username="mmm", PassW="kkkk" };
            prdb.Insert(producerInsert);
            int rowsInserted2 = prdb.SaveChanges();
            Console.WriteLine($"{rowsInserted2} rows were inserted");
            foreach (Producer pr in prList)
            {
                Console.WriteLine(pr.ToString());
            }
            //Test Delete Producer (Soft Delete)
            Console.WriteLine();
            var producerToDelete = prList.LastOrDefault();
            if (producerToDelete != null)
            {
                Console.WriteLine($"Soft deleting Producer Id:{producerToDelete.Id}");
                producerToDelete.IsActive = false;
                prdb.Delete(producerToDelete);
                int rowsDeletedProducer = prdb.SaveChanges();
                Console.WriteLine($"{rowsDeletedProducer} rows were soft deleted (Producer soft delete).");
            }
            else
            {
                Console.WriteLine("No producers to delete.");
            }

            #endregion
            Console.WriteLine("-----------------------------");
            // Test GroupDB and GroupList
            Console.WriteLine();
            Console.WriteLine("Group:");
            Console.WriteLine();
            GroupDB gdb = new();
            GroupList gList = gdb.SelectAll();
            foreach (Group g in gList)
            {
                Console.WriteLine(g.ToString());
            }
            #region Group Update, Insert, Delete Tests
            // Test Update Group
            Console.WriteLine();

            if (gList.Count > 0)
            {
                Group groupToUpdate = gList[0];
                groupToUpdate.GroupName = "Updated Group Name";
                gdb.Update(groupToUpdate);
                int rowsUpdated2 = gdb.SaveChanges();
                Console.WriteLine($"{rowsUpdated2} rows were updated");
            }
            //Test Insert Group
            Console.WriteLine(); //needs fixing in DB
            Group groupInsert = new Group { GroupName = "Inserted Group", CreationDate = DateTime.Now, IsActive = true };
            gdb.Insert(groupInsert);
            int rowsInserted5 = gdb.SaveChanges();
            Console.WriteLine($"{rowsInserted5} rows were inserted");
            foreach (Group g in gList)
            {
                Console.WriteLine(g.ToString());
            }
            //Test Delete Group (Soft Delete)-delete after fixing inserted group issue
            Console.WriteLine();
            Group groupToDelete = gList.LastOrDefault();
            if (groupToDelete != null)
            {
                Console.WriteLine($"Soft deleting: {groupToDelete.GroupName}");
                groupToDelete.IsActive = false;
                gdb.Delete(groupToDelete);
                int rowsDeleted = gdb.SaveChanges();
                Console.WriteLine($"{rowsDeleted} rows were soft deleted (Group soft delete).");
            }
            else
            {
                Console.WriteLine("No group found to delete.");
            }

            #endregion 
            Console.WriteLine("-----------------------------");
            // Test AppsDB and AppsList
            Console.WriteLine();
            Console.WriteLine("Apps:");
            Console.WriteLine();
            AppsDB adb = new AppsDB();
            AppsList aList = adb.SelectAll();
            foreach (Apps a in aList)
            {
                Console.WriteLine(a.ToString());
            }
            #region App Update, Insert Tests
            //Test Update App
            Console.WriteLine();

            if (aList.Count > 0)
            {
                Apps appToUpdate = aList[0];
                appToUpdate.AppName = "Updated App Name";
                adb.Update(appToUpdate);
                int rowsUpdated3 = adb.SaveChanges();
                Console.WriteLine($"{rowsUpdated3} rows were updated");
            }
            //Test Insert App
            Console.WriteLine();
            Apps appInsert = new Apps { AppName = "Inserted App" };
            adb.Insert(appInsert);
            int rowsInserted3 = adb.SaveChanges();
            Console.WriteLine($"{rowsInserted3} rows were inserted");
            foreach (Apps a in aList)
            {
                Console.WriteLine(a.ToString());
            }

            #endregion
            Console.WriteLine("-----------------------------");
            Console.WriteLine();
            //Test GroupMembers and GroupMembersList
            Console.WriteLine();
            Console.WriteLine("Group Members:");
            Console.WriteLine();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Yellow;
            GroupMembersDB gmdb = new GroupMembersDB();
            GroupMembersList gmList = gmdb.SelectAll();
            foreach (GroupMembers gm in gmList)
            {
                Console.WriteLine(gm.ToString());
            }
            #region Group Member Update, Insert, Delete Tests
            // Test Update Group Member
            Console.WriteLine();

            if (gmList.Count > 0)
            {
                GroupMembers groupMemberToUpdate = gmList[0];
                // Assuming we want to change the group of the member
                if (gList.Count > 1)
                {
                    groupMemberToUpdate.Group = gList[0]; // Change to another group
                    gmdb.Update(groupMemberToUpdate);
                    int rowsUpdated3 = gmdb.SaveChanges();
                    Console.WriteLine($"{rowsUpdated3} rows were updated");
                }
            }
            // Test Insert Group Member
            Console.WriteLine();
            if (pList.Count > 0 && gList.Count > 0)
            {
                GroupMembers groupMemberInsert = new GroupMembers
                {
                   
                    PassW = pList[0].PassW,
                    Username = pList[0].Username,
                    IsActive = pList[0].IsActive,
                    Group = gList[0]
                };
                gmdb.Insert(groupMemberInsert);
                int rowsInserted6 = gmdb.SaveChanges();
                Console.WriteLine($"{rowsInserted6} rows were inserted");
            }
            foreach (GroupMembers gm in gmList)
            {
                Console.WriteLine(gm.ToString());
            }
            // Test Delete Group Member (Soft Delete)
            Console.WriteLine();
            GroupMembers memberToDelete = gmList.LastOrDefault();
            if (memberToDelete != null)
            {
                Console.WriteLine($"Deleting member Id={memberToDelete.Id}, Group={memberToDelete.Group.Id}, Person={memberToDelete.Id}");
                gmdb.Delete(memberToDelete);
                int rowsDeleted = gmdb.SaveChanges();
                Console.WriteLine($"{rowsDeleted} rows were deleted (GroupMembers hard delete).");
            }
            else
            {
                Console.WriteLine("No members to delete.");
            }

            #endregion
            Console.WriteLine("-----------------------------");
            Console.WriteLine();
            // Test InstrumentDB and InstrumentList
            Console.WriteLine();
            Console.WriteLine("Instrument:");
            Console.WriteLine();
            InstrumentsDB idb = new();
            InstrumentsList iList = idb.SelectAll();
            foreach (Instruments i in iList)
            {
                Console.WriteLine(i.ToString());
            }
            #region Instrument Update, Insert Tests
            //Test Update Instrument
            Console.WriteLine();

            if (iList.Count > 0)
            {
                Instruments instrumentToUpdate = iList[0];
                instrumentToUpdate.InstrumentName = "Updated Instrument Name";
                idb.Update(instrumentToUpdate);
                int rowsUpdated4 = idb.SaveChanges();
                Console.WriteLine($"{rowsUpdated4} rows were updated");
            }
            //Test Insert Instrument
            Console.WriteLine();
            Instruments instrumentInsert = new Instruments { InstrumentName = "Inserted Instrument" };
            idb.Insert(instrumentInsert);
            int rowsInserted4 = idb.SaveChanges();
            Console.WriteLine($"{rowsInserted4} rows were inserted");
            foreach (Instruments i in iList)
            {
                Console.WriteLine(i.ToString());
            }

            #endregion
            Console.WriteLine("-----------------------------");
            // Test MusicalSegmentDB and MusicalSegmentList
            Console.WriteLine();
            Console.WriteLine("Musical Segment:");
            Console.WriteLine();
            MusicalSegmentsDB msdb = new();
            MusicalSegmentsList msList = msdb.SelectAll();
            foreach (MusicalSegments ms in msList)
            {
                Console.WriteLine(ms.ToString());
            }
            #region Musical Segment Update, Insert, Delete Tests
            // Test Update Musical Segment

            Console.WriteLine();
            if (msList.Count > 0)
            {
                MusicalSegments segmentToUpdate = msList[0];
                segmentToUpdate.SegmentName = "Updated Segment Name";
                msdb.Update(segmentToUpdate);
                int rowsUpdated5 = msdb.SaveChanges();
                Console.WriteLine($"{rowsUpdated5} rows were updated");
            }

            //Test Insert Musical Segment 
            Console.WriteLine();
            if (mList.Count > 0 && iList.Count > 0)
            {
                MusicalSegments segmentInsert = new MusicalSegments
                {
                    SegmentName = "Inserted Segment",
                    Lengthinseconds = 120,
                    Musician = mList[0],
                    Instruments = iList[0],
                    Link = null,
                    Genre = "Ambient",
                    Mood = "Calm",
                    Key = "C",
                    Bpm = 90
                };
                msdb.Insert(segmentInsert);
                int rowsInserted8 = msdb.SaveChanges();
                Console.WriteLine($"{rowsInserted8} rows were inserted");
            }
            else
            {
                Console.WriteLine("Skipping MusicalSegments insert: no Musician or Instrument available.");
            }
            foreach (MusicalSegments ms in msList)
            {
                Console.WriteLine(ms.ToString());
            }
            // Test Delete Musical Segment (Soft Delete)
            Console.WriteLine();
            var segmentToDelete = msList.LastOrDefault();
            if (segmentToDelete != null)
            {
                Console.WriteLine($"Soft deleting Musical Segment Id:{segmentToDelete.Id}, Segment Name:{segmentToDelete.SegmentName}");
                msdb.Delete(segmentToDelete);
                int rowsDeletedSegment = msdb.SaveChanges();
                Console.WriteLine($"{rowsDeletedSegment} rows were soft deleted (Musical Segment soft delete).");
            }
            else
            {
                Console.WriteLine("No musical segments to delete.");
            }

            #endregion
            Console.WriteLine("-----------------------------");
            // Test MusicianInstrumentsDB and MusicianInstrumentsList
            Console.WriteLine();
            Console.WriteLine("Musician Instruments:");
            Console.WriteLine();
            MusicianInstrumentsDB midb = new();
            MusicianInstrumentsList miList = midb.SelectAll();
            foreach (MusicianInstruments mi in miList)
            {
                Console.WriteLine(mi.ToString());
            }
            #region Musician Instrument Update, Insert, Delete Tests
            // Test Update MusicianInstrument
            Console.WriteLine();

            if (miList.Count > 0)
            {
                MusicianInstruments musicianInstrumentToUpdate = miList[0];
                // Assuming we want to change the instrument of the musician
                if (iList.Count > 1)
                {
                    musicianInstrumentToUpdate.Instruments = iList[1]; // Change to another instrument
                    midb.Update(musicianInstrumentToUpdate);
                    int rowsUpdated6 = midb.SaveChanges();
                    Console.WriteLine($"{rowsUpdated6} rows were updated");
                }
            }
            // Test Insert Musician Instrument
            Console.WriteLine();
            if (mList.Count > 0 && iList.Count > 0)
            {
                MusicianInstruments musicianInstrumentInsert = new MusicianInstruments
                {
                    Musician = mList[0],
                    Instruments = iList[0]
                };
                midb.Insert(musicianInstrumentInsert);
                int rowsInserted7 = midb.SaveChanges();
                Console.WriteLine($"{rowsInserted7} rows were inserted");
            }
            // Test Delete Musician Instrument (Soft Delete)
            Console.WriteLine();
            var musicianInstrumentToDelete = miList.LastOrDefault();
            if (musicianInstrumentToDelete != null)
            {
                Console.WriteLine($"Soft deleting MusicianInstrument Id:{musicianInstrumentToDelete.Id}");
                midb.Delete(musicianInstrumentToDelete);
                int rowsSoftDeleted = midb.SaveChanges();
                Console.WriteLine($"{rowsSoftDeleted} rows were soft deleted (MusicianInstrument soft delete).");
            }
            else
            {
                Console.WriteLine("No musician instruments to delete.");
            }

            #endregion
            Console.WriteLine("-----------------------------");
            //Test ProducerAppsDB and ProducerAppsList
            Console.WriteLine();
            Console.WriteLine("Producer Apps:");
            Console.WriteLine();
            ProducerAppsDB padb = new ProducerAppsDB();
            ProducerAppsList paList = padb.SelectAll();
            foreach (ProducerApps pa in paList)
            {
                Console.WriteLine(pa.ToString());
            }
            #region Producer App Update, Insert, Delete Tests
            // Test Update Producer App
            Console.WriteLine();

            if (paList.Count > 0)
            {
                ProducerApps producerAppToUpdate = paList[0];
                // Assuming we want to change the app of the producer
                if (aList.Count > 1)
                {
                    producerAppToUpdate.Apps = aList[1]; // Change to another app
                    padb.Update(producerAppToUpdate);
                    int rowsUpdated7 = padb.SaveChanges();
                    Console.WriteLine($"{rowsUpdated7} rows were updated");
                }
            }
            // Test Insert Producer App
            Console.WriteLine();
            if (prList.Count > 0 && aList.Count > 0)
            {
                ProducerApps producerAppInsert = new ProducerApps
                {
                    Producer = prList[0],
                    Apps = aList[0]
                };
                padb.Insert(producerAppInsert);
                int rowsInserted10 = padb.SaveChanges();
                Console.WriteLine($"{rowsInserted10} rows were inserted");
            }
            // Test Delete Producer App (Soft Delete)
            Console.WriteLine();
            var producerAppToDelete = paList.LastOrDefault();
            if (producerAppToDelete != null)
            {
                Console.WriteLine($"Deleting ProducerApp Id={producerAppToDelete.Id}, Producer={producerAppToDelete.Producer.Id}, App={producerAppToDelete.Apps.Id}");
                padb.Delete(producerAppToDelete);
                int rowsDeleted = padb.SaveChanges();
                Console.WriteLine($"{rowsDeleted} rows were deleted (ProducerApps hard delete).");
            }
            else
            {
                Console.WriteLine("No producer apps to delete.");
            }
            #endregion 
            Console.WriteLine("-----------------------------");
            Console.WriteLine();

            // Keep the console window open
            Console.ReadLine();





        }
    }
}

