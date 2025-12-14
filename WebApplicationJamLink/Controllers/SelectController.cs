using Microsoft.AspNetCore.Mvc;
using Model;
using Test;
using ViewModel;


namespace WebApplicationJamLink.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SelectController : Controller
    {
        [HttpGet]
        [ActionName("AppsSelector")]
        public AppsList AppsSelector()
        {
            AppsDB appsDB = new AppsDB();
            AppsList appsList = appsDB.SelectAll();
            return appsList;
        }

        [HttpGet]
        [ActionName("GroupSelector")]
        public GroupList GroupSelector()
        {
            GroupDB groupDB = new GroupDB();
            GroupList groupList = groupDB.SelectAll();
            return groupList;

        }

        [HttpGet]
        [ActionName("GroupMembersSelector")]
        public GroupMembersList GroupMembersSelector()
        {
            GroupMembersDB groupMembersDB = new GroupMembersDB();
            GroupMembersList groupMembersList = groupMembersDB.SelectAll();
            return groupMembersList;
        }

        [HttpGet]
        [ActionName("InstrumentsSelector")]
        public InstrumentsList InstrumentsSelector()
        {
            InstrumentsDB instrumentsDB = new InstrumentsDB();
            InstrumentsList instrumentsList = instrumentsDB.SelectAll();
            return instrumentsList;
        }
        [HttpGet]
        [ActionName("MusicalSegmentsSelector")]
        public MusicalSegmentsList MusicicalSegmentsSelector()
        {
            MusicalSegmentsDB musicicalSegmentsDB = new MusicalSegmentsDB();
            MusicalSegmentsList musicicalSegmentsList = musicicalSegmentsDB.SelectAll();
            return musicicalSegmentsList;
        }

        [HttpGet]
        [ActionName("MusiciansSelector")]
        public MusicianList MusiciansSelector()
        {
            MusicianDB musiciansDB = new MusicianDB();
            MusicianList musiciansList = musiciansDB.SelectAll();
            return musiciansList;
        }

        [HttpGet]
        [ActionName("MusicianInstrumentsSelector")]
        public MusicianInstrumentsList MusicianInstrumentsSelector()
        {
            MusicianInstrumentsDB musicianInstrumentsDB = new MusicianInstrumentsDB();
            MusicianInstrumentsList musicianInstrumentsList = musicianInstrumentsDB.SelectAll();
            return musicianInstrumentsList;
        }

        [HttpGet]
        [ActionName("PersonSelector")]
        public PersonList PersonSelector()
        {
            PersonDB personDB = new PersonDB();
            PersonList personList = personDB.SelectAll();
            return personList;
        }
        [HttpGet]
        [ActionName("ProducerSelector")]
        public PreducerList ProducerSelector()
        {
            ProducerDB producerDB = new ProducerDB();
            PreducerList producerList = producerDB.SelectAll();
            return producerList;
        }

        [HttpGet]
        [ActionName("ProducerAppsSelector")]
        public ProducerAppsList ProducerAppsSelector()
        {
            ProducerAppsDB producerAppsDB = new ProducerAppsDB();
            ProducerAppsList producerAppsList = producerAppsDB.SelectAll();
            return producerAppsList;
        }
    }
}
