
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using Leumi.Calc.Application.Services;
using Leumi.Calc.Application.Services.Dtos;

namespace Leumi.Calc.Api.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Authorize]
    public class CalcController: ControllerBase
    {
        private readonly ICalculatorService calculatorService;
        public CalcController(ICalculatorService calculatorService)
        {

            this.calculatorService = calculatorService;
        }
        /// <summary>
        /// Submit the calc values
        /// </summary>
        /// <remarks>store</remarks>
        /// <param name="body">value to be calculated</param>
        /// <response code="201">store the values on server and return its id</response>
        /// <response code="400">invalid input, object invalid</response>
        [HttpPost]
        [Route("/HCCMLeumi/Leumi.Calc/1.0.0/calc")]
        
        [SwaggerOperation("CreateCalcData")]
        [SwaggerResponse(statusCode: 201, type: typeof(Guid?), description: "store the values on server and return its id")]
        public virtual IActionResult CreateCalcData([FromBody] CalcValues body)
        { 
            var result= calculatorService.InsertCalcValues(body);
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            return StatusCode(201, result);

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
           
        }

        /// <summary>
        /// Delete a calc values identified by its id
        /// </summary>
        /// <param name="valuesId"></param>
        /// <response code="200">operation result</response>
        /// <response code="401">calc data not found</response>
        [HttpDelete]
        [Route("/HCCMLeumi/Leumi.Calc/1.0.0/calc/{valuesId}")]
        
        [SwaggerOperation("Delete")]
        public virtual IActionResult Delete([FromRoute][Required]Guid valuesId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            
            calculatorService.DeleteCalcValue(valuesId);
            return StatusCode(200);

            //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(401);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute the operation requested using the values of given id
        /// </summary>
        /// <param name="valuesId"></param>
        /// <param name="operation">0 - sum, 1 - substract, 2 - multiply, 3 - divide</param>
        /// <response code="200">operation result</response>
        /// <response code="400">bad input parameter</response>
        [HttpGet]
        [Route("/HCCMLeumi/Leumi.Calc/1.0.0/calc/{operation}/{valuesId}")]
       
        [SwaggerOperation("Result")]
        [SwaggerResponse(statusCode: 200, type: typeof(double?), description: "operation result")]
        public virtual IActionResult Result([FromRoute][Required]Guid valuesId, [FromRoute][Required]CalcOperationEnum operation)
        { 
            var result = calculatorService.ExecuteCalculation(valuesId, operation); 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            return StatusCode(200, result);

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
           
        }

        /// <summary>
        /// Update values of given calc value id
        /// </summary>
        /// <remarks>Update</remarks>
        /// <param name="body">calc value id and the new values</param>
        /// <response code="200">update</response>
        /// <response code="400">invalid input, object invalid</response>
        [HttpPut]
        [Route("/HCCMLeumi/Leumi.Calc/1.0.0/calc")]
     
        [SwaggerOperation("UpdateCalcData")]
        [SwaggerResponse(statusCode: 200, description: "update")]
        public virtual IActionResult UpdateCalcData([FromBody]CalcValues body)
        {
            calculatorService.Update(body);
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            return Ok();

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
          
        }

        /// <summary>
        /// Update the value of the property for the given calc value id
        /// </summary>
        /// <remarks>Update</remarks>
        /// <param name="body">property value to be update</param>
        /// <response code="200">ok</response>
        /// <response code="400">invalid input, object invalid</response>
        [HttpPatch]
        [Route("/HCCMLeumi/Leumi.Calc/1.0.0/calc")]
      
        [SwaggerOperation("UpdateCalcDataPartial")]
        [SwaggerResponse(statusCode: 200, type: typeof(Guid?), description: "ok")]
        public virtual IActionResult UpdateCalcDataPartial([FromBody]CalcValuesPath body)
        {
            calculatorService.Update(body);    
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            return Ok();

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);         

        }
    }
}
