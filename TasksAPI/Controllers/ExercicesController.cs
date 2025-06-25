using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TasksAPI.Controllers
{
    public class ExercicesController : Controller
    {
        List<string> fruits = new List<string>
        {
            "Apple",
            "Banana",
            "Orange",
            "Mango"
        };

        [HttpGet("fruits")]
        public ActionResult GetFruits()
        {
            string fruitsJson = JsonSerializer.Serialize(fruits);
            return Ok($"{fruitsJson}");
        }


        // Matches GET api/exercises/42
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Returned id: { id }");
        }

        [HttpGet]
        public IActionResult GetSum([FromQuery] double param1, [FromQuery] double param2)
        {
            double sum = param1 + param2;
            return Ok($"Return: { sum }");
        }

        [HttpPost("sum")]
        public IActionResult SumNumbers([FromBody] List<double> numbers)
        {
            if (numbers == null || numbers.Count == 0)
                return BadRequest("List is null or empty");



            return Ok($"The sum is: {numbers.Sum()}");
        }

        [HttpPost("update/{index}")]
        public IActionResult UpdateAtIndex(int index, [FromBody] string replacer)
        {
            if (string.IsNullOrEmpty(replacer))
            {
                return BadRequest("Replacer is null or empty");
            }

            if (index < 0 || index >= fruits.Count)
            {
                return BadRequest("Index out of bounds");
            }
            string previousFruitsJson = JsonSerializer.Serialize(fruits);

            fruits[index] = replacer;

            string fruitsJson = JsonSerializer.Serialize(fruits);
            return Ok($"Previous list: {previousFruitsJson}\n" +
                $"Replaced at index: {index}\nwith: {replacer}\nresult: {fruitsJson}");
        }

        [HttpPost("delete/{index}")]
        public IActionResult DeleteAtIndex(int index)
        {
            if (index < 0 || index >= fruits.Count)
            {
                return BadRequest("Invalid index. Index must be within the range of the list.");
            }

            string deletedItem = fruits[index];
            fruits.RemoveAt(index);

            return Ok($"Deleted item at index {index}: {deletedItem}. Updated list: {JsonSerializer.Serialize(fruits)}");
        }
    }
}
