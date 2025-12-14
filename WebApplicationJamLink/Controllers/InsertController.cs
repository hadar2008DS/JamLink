using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Text.Json;
using Test;
using ViewModel;

namespace WebApplicationJamLink.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InsertController : ControllerBase
    {
        // Post Request methods

        [HttpPost]
        [ActionName("InsertApp")]
        public IActionResult InsertApp([FromBody] Apps apps)
        {
            try
            {
                AppsDB db = new AppsDB();
                db.Insert(apps);
                int result = db.SaveChanges();

                return Ok(result); // חייב להיות רק מספר שלא תיווצר שגיאה בצד לקוח
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error inserting");
            }
        }


        [HttpPost]
        [ActionName("InsertPerson")]
        public IActionResult InsertPerson([FromBody] Person person)
        {
            try
            {
                if (person == null)
                    return BadRequest("Person object is null.");

                PersonDB personDB = new PersonDB();
                personDB.Insert(person);
                int result = personDB.SaveChanges();

                // מחזירים רק מספר כדי שהלקוח לא יקבל שגיאה
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting person: {ex.Message}");
                return StatusCode(500, "An error occurred while inserting the person.");
            }
        }


        [HttpPost]
        [ActionName("InsertProducer")]
        public IActionResult InsertProducer([FromBody] Producer producer)
        {
            try
            {
                // Check for null/undefined JSON body
                if (producer == null)
                {
                    return BadRequest("Producer object is null.");
                }

                // Insert into DB and return numeric result
                ProducerDB producerDB = new ProducerDB();
                producerDB.Insert(producer);
                int result = producerDB.SaveChanges();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting producer: {ex.Message}");
                return StatusCode(500, "An error occurred while inserting the producer.");
            }
        }

        [HttpPost]
        [ActionName("InsertMusician")]
        public IActionResult InsertMusician([FromBody] Musician musician)
        {
            try
            {
                if (musician == null)
                    return BadRequest("Musician object is null.");

                MusicianDB musiciansDB = new MusicianDB();
                musiciansDB.Insert(musician);

                int rowsAffected = musiciansDB.SaveChanges();

                if (rowsAffected > 0)
                {
                    return Ok(rowsAffected);
                }
                else
                {
                    return StatusCode(500, "Failed to insert musician.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting musician: {ex.Message}");
                return StatusCode(500, "An error occurred while inserting the musician.");
            }
        }

        [HttpPost]
        [ActionName("InsertGroup")]
        public IActionResult InsertGroup([FromBody] Group group)
        {
            try
            {
                if (group == null)
                    return BadRequest("Group object is null.");

                GroupDB groupDB = new GroupDB();
                groupDB.Insert(group);
                int result = groupDB.SaveChanges();

                if (result > 0)
                    return Ok(result);
                //return Ok($"Group with ID {group.Id} inserted successfully.");
                else
                    return StatusCode(500, "Failed to insert group.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting group: {ex.Message}");
                return StatusCode(500, "An error occurred while inserting the group.");
            }
        }

        [HttpPost]
        [ActionName("InsertMusicalSegment")]
        public IActionResult InsertMusicalSegment([FromBody] MusicalSegments musicalSegment)
        {
            try
            {
                if (musicalSegment == null)
                    return BadRequest("MusicalSegment object is null.");

                MusicalSegmentsDB musicalSegmentsDB = new MusicalSegmentsDB();
                musicalSegmentsDB.Insert(musicalSegment);
                int result = musicalSegmentsDB.SaveChanges();

                if (result > 0)
                    return Ok(result);
                    //return Ok($"Musical segment with ID {musicalSegment.Id} inserted successfully.");
                else
                    return StatusCode(500, "Failed to insert musical segment.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting musical segment: {ex.Message}");
                return StatusCode(500, "An error occurred while inserting the musical segment.");
            }
        }

        [HttpPost]
        [ActionName("InsertInstrument")]
        public IActionResult InsertInstrument([FromBody] Instruments instrument)
        {
            try
            {
                if (instrument == null)
                    return BadRequest("Instrument object is null.");

                InstrumentsDB instrumentsDB = new InstrumentsDB();
                instrumentsDB.Insert(instrument);
                int result = instrumentsDB.SaveChanges();

                if (result > 0)
                    return Ok(result);
              //  return Ok($"Instrument with ID {instrument.Id} inserted successfully.");
                else
                    return StatusCode(500, "Failed to insert instrument.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting instrument: {ex.Message}");
                return StatusCode(500, "An error occurred while inserting the instrument.");
            }
        }

        [HttpPost]
        [ActionName("InsertGroupMember")]
        public IActionResult InsertGroupMember([FromBody] GroupMembers member)
        {
            if (member == null)
                return BadRequest("GroupMember object is null.");


            try
            {
                GroupMembersDB db = new GroupMembersDB();
                db.Insert(member);
                int result = db.SaveChanges();
                if (result > 0)
                    return Ok(result);
                else
                    return StatusCode(500, "Failed to insert group-member relation.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting group-member relation: {ex}");
                return StatusCode(500, "An error occurred while inserting the group-member relation.");
            }
        }




        [HttpPost]
        [ActionName("InsertMusicianInstrument")]
        public IActionResult InsertMusicianInstrument([FromBody] MusicianInstruments musicianInstrument)
        {
            try
            {
                if (musicianInstrument == null)
                    return BadRequest("MusicianInstrument object is null.");

                MusicianInstrumentsDB musicianInstrumentsDB = new MusicianInstrumentsDB();
                musicianInstrumentsDB.Insert(musicianInstrument);
                int result = musicianInstrumentsDB.SaveChanges();

                if (result > 0)
                    return Ok(result);
                //return Ok($"Musician-instrument relation with ID {musicianInstrument.Id} inserted successfully.");
                else
                    return StatusCode(500, "Failed to insert musician-instrument relation.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting musician-instrument: {ex.Message}");
                return StatusCode(500, "An error occurred while inserting the musician-instrument relation.");
            }
        }

        [HttpPost]
        [ActionName("InsertProducerApp")]
        public IActionResult InsertProducerApp([FromBody] ProducerApps producerApp)
        {
            try
            {
                if (producerApp == null)
                    return BadRequest("ProducerApp object is null.");

                ProducerAppsDB producerAppsDB = new ProducerAppsDB();
                producerAppsDB.Insert(producerApp);

                // SaveChanges returns the number of rows affected
                int rowsAffected = producerAppsDB.SaveChanges();

                if (rowsAffected > 0)
                {
                    // Return number of rows affected instead of the ID
                    return Ok(rowsAffected); // <-- now returns 1
                }
                else
                {
                    return StatusCode(500, "Failed to insert producer-app relation.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting producer-app relation: {ex.Message}");
                return StatusCode(500, "An error occurred while inserting the producer-app relation.");
            }
        }

    }
}
