using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Test;
using ViewModel;

namespace WebApplicationJamLink.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        // PUT controller

        [HttpPut]
        [ActionName("UpdateApp")]
        public IActionResult UpdateApp([FromBody] Apps apps)
        {
            try
            {
                if (apps == null)
                    return BadRequest("Apps object is null.");

                AppsDB appsDB = new AppsDB();
                appsDB.Update(apps);
                int result = appsDB.SaveChanges();

                if (result > 0)
                    //return Ok($"App with ID {apps.Id} updated successfully.");
                    return Ok(result);
                else
                    return NotFound($"No app found with ID {apps.Id}.");
            }
            catch (Exception ex)
            {
                // Log error if you have a logging system
                Console.WriteLine($"Error updating app: {ex.Message}");
                // Return a generic error to the client (do not expose sensitive details)
                return StatusCode(500, "An error occurred while updating the app.");
            }
        }

        [HttpPut]
        [ActionName("UpdatePerson")]
        public IActionResult UpdatePerson([FromBody] Person person)
        {
            try
            {
                if (person == null)
                    return BadRequest("Person object is null.");

                PersonDB personDB = new PersonDB();
                personDB.Update(person);
                int result = personDB.SaveChanges();

                if (result > 0)
                    //return Ok($"Person with ID {person.Id} updated successfully.");
                    return Ok(result);
                else
                    return NotFound($"No person found with ID {person.Id}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating person: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the person.");
            }
        }

        [HttpPut]
        [ActionName("UpdateProducer")]
        public IActionResult UpdateProducer([FromBody] Producer producer)
        {
            try
            {
                if (producer == null)
                    return BadRequest("Producer object is null.");

                ProducerDB producerDB = new ProducerDB();
                producerDB.Update(producer);
                int result = producerDB.SaveChanges();

                if (result > 0)
                    //return Ok($"Producer with ID {producer.Id} updated successfully.");
                    return Ok(result);
                else
                    return NotFound($"No producer found with ID {producer.Id}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating producer: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the producer.");
            }
        }

        [HttpPut]
        [ActionName("UpdateMusician")]
        public IActionResult UpdateMusician([FromBody] Musician musician)
        {
            try
            {
                if (musician == null)
                    return BadRequest("Musician object is null.");

                MusicianDB musicianDB = new MusicianDB();
                musicianDB.Update(musician);
                int result = musicianDB.SaveChanges();

                if (result > 0)
                    //return Ok($"Musician with ID {musician.Id} updated successfully.");
                    return Ok(result);
                else
                    return NotFound($"No musician found with ID {musician.Id}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating musician: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the musician.");
            }
        }

        [HttpPut]
        [ActionName("UpdateGroup")]
        public IActionResult UpdateGroup([FromBody] Group group)
        {
            try
            {
                if (group == null)
                    return BadRequest("Group object is null.");

                if (group.Id <= 0)
                    return BadRequest("Invalid Group ID.");

                GroupDB groupDB = new GroupDB();
                groupDB.Update(group);
                int result = groupDB.SaveChanges();

                if (result > 0)
                    //return Ok($"Group with ID {group.Id} updated successfully.");
                    return Ok(result);
                else
                    return StatusCode(500, "Failed to update group. Maybe ID does not exist.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating group: {ex}");
                return StatusCode(500, $"An error occurred while updating the group: {ex.Message}");
            }
        }

        [HttpPut]
        [ActionName("UpdateMusicalSegment")]
        public IActionResult UpdateMusicalSegment([FromBody] MusicalSegments musicalSegment)
        {
            try
            {
                if (musicalSegment == null)
                    return BadRequest("MusicalSegment object is null.");

                MusicalSegmentsDB musicalSegmentsDB = new MusicalSegmentsDB();
                musicalSegmentsDB.Update(musicalSegment);
                int result = musicalSegmentsDB.SaveChanges();

                if (result > 0)
                    //return Ok($"Musical segment with ID {musicalSegment.Id} updated successfully.");
                    return Ok(result);
                else
                    return NotFound($"No musical segment found with ID {musicalSegment.Id}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating musical segment: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the musical segment.");
            }
        }

        [HttpPut]
        [ActionName("UpdateInstrument")]
        public IActionResult UpdateInstrument([FromBody] Instruments instrument)
        {
            try
            {
                if (instrument == null)
                    return BadRequest("Instrument object is null.");

                InstrumentsDB instrumentsDB = new InstrumentsDB();
                instrumentsDB.Update(instrument);
                int result = instrumentsDB.SaveChanges();

                if (result > 0)
                    //return Ok($"Instrument with ID {instrument.Id} updated successfully.");
                    return Ok(result);
                else
                    return NotFound($"No instrument found with ID {instrument.Id}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating instrument: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the instrument.");
            }
        }

        [HttpPut]
        [ActionName("UpdateGroupMember")]
        public IActionResult UpdateGroupMember([FromBody] GroupMembers groupMember)
        {
            try
            {
                if (groupMember == null)
                    return BadRequest("GroupMember object is null.");

                GroupMembersDB groupMembersDB = new GroupMembersDB();
                groupMembersDB.Update(groupMember);
                int result = groupMembersDB.SaveChanges();

                if (result > 0)
                    //return Ok($"Group member with ID {groupMember.Id} updated successfully.");
                    return Ok(result);
                else
                    return NotFound($"No group member found with ID {groupMember.Id}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating group member: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the group member.");
            }
        }

        [HttpPut]
        [ActionName("UpdateMusicianInstrument")]
        public IActionResult UpdateMusicianInstrument([FromBody] MusicianInstruments musicianInstrument)
        {
            try
            {
                if (musicianInstrument == null)
                    return BadRequest("MusicianInstrument object is null.");

                MusicianInstrumentsDB musicianInstrumentsDB = new MusicianInstrumentsDB();
                musicianInstrumentsDB.Update(musicianInstrument);
                int result = musicianInstrumentsDB.SaveChanges();

                if (result > 0)
                    //return Ok($"Musician-instrument relation with ID {musicianInstrument.Id} updated successfully.");
                    return Ok(result);
                else
                    return NotFound($"No musician-instrument relation found with ID {musicianInstrument.Id}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating musician-instrument: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the musician-instrument relation.");
            }
        }

        [HttpPut]
        [ActionName("UpdateProducerApp")]
        public IActionResult UpdateProducerApp([FromBody] ProducerApps producerApp)
        {
            try
            {
                if (producerApp == null)
                    return BadRequest("ProducerApp object is null.");

                ProducerAppsDB producerAppsDB = new ProducerAppsDB();
                producerAppsDB.Update(producerApp);
                int result = producerAppsDB.SaveChanges();

                if (result > 0)
                    //return Ok($"Producer-app relation with ID {producerApp.Id} updated successfully.");
                    return Ok(result);
                else
                    return NotFound($"No producer-app relation found with ID {producerApp.Id}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating producer-app relation: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the producer-app relation.");
            }
        }
    }
}

