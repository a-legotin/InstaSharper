using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class CreativeConfigResponse
    {
        [JsonProperty("capture_type")]             public string CaptureType  { get; set; }

        [JsonProperty("face_effect_id")]           public long FaceEffectId   { get; set; }

        [JsonProperty("should_rendert_try_it_on")] public bool ShoulRender    { get; set; }

        [JsonProperty("camera_facing")]            public string CameraFacing { get; set; }
    }
}
