using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Test;
using ViewModel;

namespace WebApplicationJamLink.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        [HttpDelete]
        [ActionName("DeleteGroup")]
        public IActionResult DeleteGroup([FromBody] int  groupId)
        {
            GroupDB groupDB = new GroupDB();
            groupDB.Delete(new Group { Id = groupId }); // Pass the Group object
            int result = groupDB.SaveChanges();
            if (result == 0)
                return NotFound("Group not found");

            return Ok(result);
        }

        [HttpDelete]
        [ActionName("DeleteGroupMembers")]
        public IActionResult DeleteGroupMembers([FromBody] int groupMemberId)
        {
            GroupMembersDB groupMembersDB = new GroupMembersDB();
            groupMembersDB.Delete(new GroupMembers { Id = groupMemberId }); // Pass the GroupMembers object
            int result = groupMembersDB.SaveChanges();
            if (result == 0)
                return NotFound("Group member not found");

            return Ok(result);
        }

        [HttpDelete]
        [ActionName("DeleteMusicalSegment")]
        public IActionResult DeleteMusicalSegment([FromBody] int segmentId)
        {
            MusicalSegmentsDB musicalSegmentsDB = new MusicalSegmentsDB();
            musicalSegmentsDB.Delete(new MusicalSegments { Id = segmentId }); // Pass the MusicalSegments object with Id
            int result = musicalSegmentsDB.SaveChanges();
            if (result == 0)
                return NotFound("Musical segment not found");

            return Ok(result);
        }

        [HttpDelete]
        [ActionName("DeleteMusician")]
        public IActionResult DeleteMusician([FromBody] int musicianId)
        {
            MusicianDB musicianDB = new MusicianDB();
            musicianDB.Delete(new Musician { Id = musicianId }); // Pass the Musician object with Id
            int result = musicianDB.SaveChanges();
            if (result == 0)
                return NotFound("Musician not found");

            return Ok(result);
        }


        [HttpDelete]
        [ActionName("DeleteMusicianInstrument")]
        public IActionResult DeleteMusicianInstrument([FromBody] int musicianInstrumentId)
        {
            MusicianInstrumentsDB musicianInstrumentsDB = new MusicianInstrumentsDB();
            musicianInstrumentsDB.Delete(new MusicianInstruments { Id = musicianInstrumentId }); // Pass the object with Id
            int result = musicianInstrumentsDB.SaveChanges();
            if (result == 0)
                return NotFound("Musician instrument not found");

            return Ok(result);
        }


        [HttpDelete]
        [ActionName("DeletePerson")]
        public IActionResult DeletePerson([FromBody] int personId)
        {
            PersonDB personDB = new PersonDB();
            personDB.Delete(new Person { Id = personId }); // Pass the Person object
            int result = personDB.SaveChanges();
            if (result == 0)
                return NotFound("Person not found");

            return Ok(result);
        }

        [HttpDelete]
        [ActionName("DeleteProducer")]
        public IActionResult DeleteProducer([FromBody] int producerId)
        {
            ProducerDB producerDB = new ProducerDB();
            producerDB.Delete(new Producer { Id = producerId }); // Pass the Producer object with Id
            int result = producerDB.SaveChanges();
            if (result == 0)
                return NotFound("Producer not found");

            return Ok(result);
        }

        [HttpDelete]
        [ActionName("DeleteProducerApp")]
        public IActionResult DeleteProducerApp([FromBody] int producerAppId)
        {
            ProducerAppsDB producerAppsDB = new ProducerAppsDB();
            producerAppsDB.Delete(new ProducerApps { Id = producerAppId }); // Pass the object with Id
            int result = producerAppsDB.SaveChanges();
            if (result == 0)
                return NotFound("Producer app not found");

            return Ok(result);
        }



    }
}
