using System;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace intexii.Data
{
    public class APIData
    {
        public float id { get; set; }
        public float squarenorthsouth { get; set; }
        public float depth { get; set; }
        public float southtohead { get; set; }
        public float squareeastwest { get; set; }
        public float westtohead { get; set; }
        public float westtofeet { get; set; }
        public float southtofeet { get; set; }
        public float eastwest_W { get; set; }
        public float adultsubadult_C { get; set; }
        public float adultsubadult_Other { get; set; }
        public float samplescollected_false { get; set; }
        public float samplescollected_true { get; set; }
        public float area_NW { get; set; }
        public float area_Other { get; set; }
        public float area_SE { get; set; }
        public float area_SW { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                id, squarenorthsouth, depth, southtohead, squareeastwest, westtohead,
                westtofeet, southtofeet, eastwest_W, adultsubadult_C, adultsubadult_Other,
                samplescollected_false, samplescollected_true, area_NW, area_Other, area_SE, area_SW
                };
            int[] dimensions = new int[] { 1, 17 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}