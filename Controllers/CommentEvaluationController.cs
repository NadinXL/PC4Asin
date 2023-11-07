using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;

namespace PC4Asin.Controllers
{
    public class CommentEvaluationController : Controller
    {
        private readonly ILogger<CommentEvaluationController> _logger;
        private readonly PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> _predictionEnginePool;

        public CommentEvaluationController(ILogger<CommentEvaluationController> logger, PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;
        }

        public IActionResult Index()
        
        {
            return View("Views/MLModel/Index.cshtml"); // Especifica la ubicación completa de la vista
        }

        [HttpPost]
        public IActionResult EvaluarComentario(string comentario)
        {
            var input = new MLModel1.ModelInput
            {
                Col0 = comentario
            };

            // Realiza una predicción usando el modelo ML.NET
            MLModel1.ModelOutput prediction = _predictionEnginePool.Predict(input);

            // El resultado de la predicción está en prediction.PredictedLabel
            // Puedes procesar el resultado según tus necesidades
            if(prediction.PredictedLabel==1){
            ViewBag.Resultado = "Es un comentario positivo (1)";}
            else{
            ViewBag.Resultado = "Es un comentario negativo (0)";
            }

             return View("Views/MLModel/Resultado.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}