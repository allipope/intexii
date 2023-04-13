using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using intexii.Data;
using intexii.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace intexii.Controllers
{
    [ApiController]
    [Route("/score")]
    public class APIController : ControllerBase
    {
        private InferenceSession _session;
        public APIController(InferenceSession session)
        {
            _session = session;
        }
        [HttpPost]
        public IActionResult Score(APIData data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });
            Tensor<string> score = result.First().AsTensor<string>();
            var prediction = new Prediction { PredictedValue = score.First() };
            result.Dispose();
            return new JsonResult(prediction);
        }
    }
}