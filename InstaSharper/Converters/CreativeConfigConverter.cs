using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using System;

namespace InstaSharper.Converters
{
    public class CreativeConfigConverter : IObjectConverter<CreativeConfig, CreativeConfigResponse>
    {
        public CreativeConfigResponse SourceObject { get ; set ; }

        public CreativeConfig Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"{SourceObject}");

            return new CreativeConfig
            {
                CameraFacing = SourceObject.CameraFacing,
                CaptureType  = SourceObject.CaptureType,
                FaceEffectId = SourceObject.FaceEffectId,
                ShoulRender  = SourceObject.ShoulRender
            };
        }
    }
}
