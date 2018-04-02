using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace InstaSharper.Classes
{
    //{
    //	"video_upload_urls": [{
    //		"url": "https://upload-ec2.instagram.com/api/v1/stage/video/?target=FTW\u0026vx_token=0\u0026staging_in_tao=0",
    //		"job": "AQBoXxbLKsdX-WC3v8I3Jcp3uTWqpCJwH8OjLJaK9l_JJQ7s2FabtUOpaQgeZWiBssIjOPdzzyOE9hYg_AtVBOAlc6hUFXaXVqMpo_25xbI618brCO98WD1NwkfsP0DXNih5xhutiyYX4qfQJaMfgJNLmOpuWTFlfCpK_aEuZVyNpCFk8yzexDlQk-DRPZzdQlvO3xitKeTTAeDbnDJsGISZEEHFdZfhyuT7_Gam06d-XX5lqfr2kzyg6bsLClVOF4LpdrCDq-oIy-z2T-qIOOhJyt2-FsstyC7GImV-6lsj2VZRYqTNTFGXL7TgMTVWD29k8Y90AYJkqffqPe8YKi30jm1aC1le6pXRS-TrnyU_aA",
    //		"expires": 1837773555.8330443
    //	}, {
    //		"url": "https://upload-ec2.instagram.com/api/v1/stage/video/?target=FTW\u0026vx_token=4\u0026staging_in_tao=0",
    //		"job": "AQAtauql6f5dI523sc_KangZH0-U193N2Q8omZEbPLhwb6o80y5J-8y-bHvDCqs231d-_lB5KD8HiTxcPlDaEeOu-zgURSGa7pP2Am6GwSMKYPvyvujm3n6o7rZQKjkQ1ktrB3-u3gFSFCokmHMXkT5jiV-TDn3mWbxPQROluB7zVIpgMY2QVMii-SpnxMDpz81xVI1OwgoL2IbqmJMCrYHVba8LAlo-5Is-wxrGYKTw7zzFP8weW0qAWI6XSVfEN7LFL4_LP8UKvo5X4MCNjE5YgrZpYmIkDF4awUNDZLN5yu5u8ZnNhLL7zzkJZQYhFlACVZv77P54waIxJlHnl8E-ARwLScGNJxBDWQ1c5o3Lsg",
    //		"expires": 1837773555.8332503
    //	}, {
    //		"url": "https://upload-ec2.instagram.com/api/v1/stage/video/?target=FTW\u0026vx_token=2\u0026staging_in_tao=0",
    //		"job": "AQDAEL3nwefP153_t9eheC4SGbKTVJdBUBXmYlgCxGfcGe_bP-CNdl2Qa7rKKILr4bTM-hXPJ9DMBJM6AGhiNQUoEgEOsR3aiyHqTl0c9j38u_B6mar0FFaouyjqPrgIsc6bb3-ItWc2cgygjATCzbCB4Ieee01iAoxAMXRWSHzbxm5dw-ZABppvjFGH90BgA1aCwCOJaQ9gresqq1o37aapUCmnUWeDx4i90PQbNav7HWKh5uWQWK-1kSCP9HWUbRLs7bRwVUcUsezhZRS5oNin2y7S2IlgpvPJkUrp9581JsUMYViKh-YtSJ2YDgcmdqoes4NbbNGbu-WFVy-OQ7GQcb1RXmATq7Sh75YgqACM8A",
    //		"expires": 1837773555.8334284
    //	}, {
    //		"url": "https://upload-ec2.instagram.com/api/v1/stage/video/?target=FTW\u0026vx_token=3\u0026staging_in_tao=0",
    //		"job": "AQD2AEK1PPjRPy0RKZmyIczo4usyJypPp6NRHfly2MDosVezEKjD7JY2sGRxi5kZgbqw0CUZsKKc4LLFgOLVJxBfM4lrQeDihDvg_D9bTUqR21gCMS07qmoCqgXF_mQ8e5fLeTWNCUT0HAXeStRlNqsc5XgAOo5d3LMmie8owB-_4X5qroXtBcZoMxXUuW_ye23-CM23BBG9d5dd4tFPnRR76Ku72L_wex14rHzP-xMQf52hgdayS560hg2MPlGlKVknXrE4d3Grz3asBbbEkr-OHNpKyshvrW4g2y_CNZbPYLGAxrwjO3QFK6UNeyoMcvbszeVYur9FAeXHY8Dg0wQD_9ZY9rWwBiHl5iJC5sNyQQ",
    //		"expires": 1837773555.8336244
    //	}],
    //	"upload_id": "1522413553",
    //	"xsharing_nonces": {},
    //	"status": "ok"
    //}


    public class VideoUploadJobResponse
    {
        [JsonProperty("video_upload_urls")]
        public List<VideoUploadUrl> VideoUploadUrls { get; set; }
        [JsonProperty("upload_id")]
        public string UploadId { get; set; }
        [JsonProperty("xsharing_nonces")]
        public object XSharingNonces { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class VideoUploadUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("job")]
        public string Job { get; set; }
        [JsonProperty("expires")]
        public double Expires { get; set; }
    }

    //public class XsharingNonces
    //{
    //}
}
