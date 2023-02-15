using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Mvc;



namespace API.Controllers
{

    public class ActivitiesController : BaseApiController
    {
       
        [HttpGet]// api/activities
        public async Task<IActionResult> GetActivites()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]// api/activities/abcballala
        public async Task<IActionResult> GetActivity(Guid id)
        {

            return HandleResult(await Mediator.Send(new Details.Query{Id=id}));
            // When not Using Clean Architecture, logic inside controller u can use this code for validation
            // var activity=await Mediator.Send(new Details.Query{Id=id});
            // if(activity == null) return NotFound();
            // return activity; 
           
        }

      
      [HttpPost]
      public async Task<IActionResult> CreateActivity(Activity activity)
      {
        return HandleResult(await Mediator.Send(new Create.Command{Activity=activity}));
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> EditActivity(Guid id,Activity activity)
      {
        activity.Id = id; //membuat persamaan antara id dari controller dengan database
        return HandleResult(await Mediator.Send(new Edit.Command{Activity=activity}));
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteActivity(Guid id)
      {
        return HandleResult(await Mediator.Send(new Delete.Command{Id=id}));
      }

      [HttpPost("{id}/attend")] //used for canceling the activity 
      public async  Task<IActionResult> Attend (Guid id)
      {
        return HandleResult(await Mediator.Send(new UpdateAttendance.Command{Id=id}));
      }

    }
}