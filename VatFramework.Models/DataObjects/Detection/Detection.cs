using System;
using System.Collections.Generic;
using System.Text;

namespace FRSCInventory.Models.DataObjects.Detection
{
  public  class Detection
    {
        public string language { get; set; }
        public bool isReliable { get; set; }
        public float confidence { get; set; }
    }

    public class ResultData
    {
        public List<Detection> detections { get; set; }
    }


    public class Result
    {
        public ResultData data { get; set; }
    }

    #region classes
    public class Translation
    {
        public string translatedText { get; set; }
        public string detectedSourceLanguage { get; set; }
    }

    public class Data
    {
        public List<Translation> translations { get; set; }
    }

    public class RootObject
    {
        public Data data { get; set; }
    }
    #endregion


    public class DataDetect
    {
        public List<List<Dictionary<string, string>>> detections { get; set; }
    }

    public class RootObjectDetect
    {
        public DataDetect data { get; set; }
    }

    public class DetectionData
    {
        public float confidence { get; set; }
        public bool isReliable { get; set; }
        public string language { get; set; }
    }

}
