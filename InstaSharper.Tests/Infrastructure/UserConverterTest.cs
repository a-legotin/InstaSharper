using System.Linq;
using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Helpers;
using Newtonsoft.Json;
using Xunit;

namespace InstaSharper.Tests.Infrastructure
{
    [Trait("Category", "Infrastructure")]
    public class UserConverterTest
    {
        private const string testJson = @"{
	""num_results"": 36,
	""users"": [{
			""pk"": 1816494776,
			""username"": ""codziennysuchar"",
			""full_name"": ""Codzienny Suchar Humor Memy"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/11244021_1016245205072659_328649662_a.jpg"",
			""friendship_status"": {
				""following"": true,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 463666,
			""byline"": ""463k followers"",
			""social_context"": ""Following"",
			""search_social_context"": ""Following"",
			""mutual_followers_count"": 8.0,
			""unseen_count"": 1
		}, {
			""pk"": 4390037188,
			""username"": ""_najlepsze.suchary"",
			""full_name"": ""Codzienny_suchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/14582494_1641939916100038_3059248674581250048_n.jpg"",
			""profile_pic_id"": ""1423287750286768474_4390037188"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 3398,
			""byline"": ""3398 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2071863124,
			""username"": ""codzienny_suchar"",
			""full_name"": ""codzienny suchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/10518138_478803852289501_1249129511_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 565,
			""byline"": ""565 followers"",
			""mutual_followers_count"": 4.23
		}, {
			""pk"": 2292120036,
			""username"": ""codziennysuchar_"",
			""full_name"": ""CodziennySucharek\u00a2"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/12071063_791734954269121_1518685845_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 85,
			""byline"": ""85 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 4299660758,
			""username"": ""codziennysuchar143"",
			""full_name"": ""#codziennysuchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/14712311_1386416128048966_6072005659623161856_a.jpg"",
			""profile_pic_id"": ""1410766283506563741_4299660758"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 45,
			""byline"": ""45 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 6223038622,
			""username"": ""_codzienny_suchar_"",
			""full_name"": ""codzienny suchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/22580089_357978407993952_2464035214595194880_n.jpg"",
			""profile_pic_id"": ""1626063587179128120_6223038622"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 32,
			""byline"": ""32 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 3186683532,
			""username"": ""_codzienny_suchar._"",
			""full_name"": """",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/13129611_465250017018877_1876717231_a.jpg"",
			""profile_pic_id"": ""1239795253686777462_3186683532"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 40,
			""byline"": ""40 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 3479771356,
			""username"": ""_codziennysuchar_"",
			""full_name"": ""\ud83d\ude00 Codzienny suchar! \ud83d\ude00"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/13531793_1617545068557916_1945776659_a.jpg"",
			""profile_pic_id"": ""1283581678362859682_3479771356"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 19,
			""byline"": ""19 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2969669697,
			""username"": ""codzienny__suchar"",
			""full_name"": ""codziennysuchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/11349423_955148704532172_366023392_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 49,
			""byline"": ""49 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2267989776,
			""username"": ""codziennysuchar1"",
			""full_name"": ""codziennysuchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://instagram.ftxl1-1.fna.fbcdn.net/t51.2885-19/11906329_960233084022564_1448528159_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": true,
			""follower_count"": 10,
			""byline"": ""10 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2365761043,
			""username"": ""codziennysuchareg"",
			""full_name"": ""codziennysuchareg"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/12353364_846217685497843_396153855_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 13,
			""byline"": ""13 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 4918495064,
			""username"": ""codziennysuchar10"",
			""full_name"": ""Codzienny Suchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/17265880_157360331452947_2547629150919720960_a.jpg"",
			""profile_pic_id"": ""1479246617067586083_4918495064"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 40,
			""byline"": ""40 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2898853357,
			""username"": ""jdisowskyyx"",
			""full_name"": ""Codziennysuchar LOL\ud83d\ude1c\ud83d\ude04\ud83d\ude0e"",
			""is_private"": true,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/13092489_1032479350180187_480622312_a.jpg"",
			""profile_pic_id"": ""1240517337186335492_2898853357"",
			""friendship_status"": {
				""following"": false,
				""is_private"": true,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 24,
			""byline"": ""24 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2288964948,
			""username"": ""codzienny.suchar"",
			""full_name"": ""Suchar Codzienny"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/12269875_1617130481885815_92792324_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 64,
			""byline"": ""64 followers"",
			""mutual_followers_count"": ""wtf going on here 10""
		}, {
			""pk"": 2869035614,
			""username"": ""codzienny.sucharek"",
			""full_name"": ""Codzienny.sucharek"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/12547256_584493841706454_1681254975_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 21,
			""byline"": ""21 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 4373567288,
			""username"": ""codzienny_suchar_dnia"",
			""full_name"": ""Klara"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/15803493_328997667500308_2950628599478091776_a.jpg"",
			""profile_pic_id"": ""1422395872512007684_4373567288"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 6,
			""byline"": ""6 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 3141115423,
			""username"": ""taylornator1989"",
			""full_name"": ""POLECAM codziennysuchar!!!"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/13187945_283320095335534_1866674286_a.jpg"",
			""profile_pic_id"": ""1260222356237230343_3141115423"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 35,
			""byline"": ""35 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 4670037348,
			""username"": ""codzienny___suchar"",
			""full_name"": ""Suchary i Memy"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/16465599_1842829409324069_1856675956463239168_a.jpg"",
			""profile_pic_id"": ""1453492070248861927_4670037348"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 10,
			""byline"": ""10 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 3593042169,
			""username"": ""_codziennysuchar"",
			""full_name"": ""_codziennysuchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/13767601_920771888051908_1782552445_a.jpg"",
			""profile_pic_id"": ""1303857380802939487_3593042169"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 16,
			""byline"": ""16 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 6021270917,
			""username"": ""codzienny_suchar2004"",
			""full_name"": "".."",
			""is_private"": true,
			""profile_pic_url"": ""https://instagram.ftxl1-1.fna.fbcdn.net/t51.2885-19/11906329_960233084022564_1448528159_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": true,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": true,
			""follower_count"": 10,
			""byline"": ""10 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 3431371153,
			""username"": ""codzienny.suchar_"",
			""full_name"": ""Codzienny Suchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/13385911_1737718929816684_1453121998_a.jpg"",
			""profile_pic_id"": ""1276045232836840047_3431371153"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 8,
			""byline"": ""8 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2981506650,
			""username"": ""_codzienny_suchar"",
			""full_name"": """",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/10817631_479740075550561_1982476853_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 10,
			""byline"": ""10 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 5892966471,
			""username"": ""codzienny_suchareczek"",
			""full_name"": ""\ud83d\ude02codzinna dawka \u015bmiechu\ud83d\ude02"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/20904967_1343774645741537_2922481076037222400_a.jpg"",
			""profile_pic_id"": ""1583925224255829184_5892966471"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 18,
			""byline"": ""18 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 3015019252,
			""username"": ""codzienny__sucharek"",
			""full_name"": """",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/10632224_471179623073948_2122634524_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 4,
			""byline"": ""4 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2141568556,
			""username"": ""codzienny_suchar_1"",
			""full_name"": ""codzienny Suchar"",
			""is_private"": true,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/12276961_1527174690937301_745676031_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": true,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 64,
			""byline"": ""64 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2248225276,
			""username"": ""codzienny_sucharek"",
			""full_name"": """",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/12338890_556623717824871_1971485021_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 88,
			""byline"": ""88 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2182521594,
			""username"": ""codziennysucharek"",
			""full_name"": ""codziennysucharek"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/11950493_1021972774500543_1552776432_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 26,
			""byline"": ""26 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 2246594474,
			""username"": ""codzienny_suchar_"",
			""full_name"": ""suchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://instagram.ftxl1-1.fna.fbcdn.net/t51.2885-19/11906329_960233084022564_1448528159_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": true,
			""follower_count"": 6,
			""byline"": ""6 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 3030330532,
			""username"": ""_____codziennysuchar_____"",
			""full_name"": """",
			""is_private"": false,
			""profile_pic_url"": ""https://instagram.ftxl1-1.fna.fbcdn.net/t51.2885-19/11906329_960233084022564_1448528159_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": true,
			""follower_count"": 0,
			""byline"": ""0 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 3281163757,
			""username"": ""codziennysuchar2"",
			""full_name"": ""Suchar Codzienny"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/13258846_1692403691024457_272839865_a.jpg"",
			""profile_pic_id"": ""1261036094132762712_3281163757"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 1,
			""byline"": ""1 follower"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 3214950254,
			""username"": ""codziennysucharekkk"",
			""full_name"": ""codzienny-sucharek"",
			""is_private"": false,
			""profile_pic_url"": ""https://instagram.ftxl1-1.fna.fbcdn.net/t51.2885-19/11906329_960233084022564_1448528159_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": true,
			""follower_count"": 0,
			""byline"": ""0 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 3698151442,
			""username"": ""codziennysuchar8"",
			""full_name"": ""Suchar Dnia"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/14073336_1683619201961289_1832154875_a.jpg"",
			""profile_pic_id"": ""1322280126667234202_3698151442"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 6,
			""byline"": ""6 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 5352527941,
			""username"": ""codzienny_suchar.pl"",
			""full_name"": ""Sucharek"",
			""is_private"": true,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/17934680_1433837510000277_1688326230736109568_a.jpg"",
			""profile_pic_id"": ""1496469540404649383_5352527941"",
			""friendship_status"": {
				""following"": false,
				""is_private"": true,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 0,
			""byline"": ""0 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 5612429408,
			""username"": ""codzienny_suchar1"",
			""full_name"": ""codzienny suchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-frt3-2.cdninstagram.com/t51.2885-19/s150x150/19227720_307764163005012_1840796191059607552_a.jpg"",
			""profile_pic_id"": ""1539108474653437325_5612429408"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": false,
			""follower_count"": 0,
			""byline"": ""0 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 5571319582,
			""username"": ""codzienny_sucharxddd"",
			""full_name"": ""Codzienny suchar"",
			""is_private"": false,
			""profile_pic_url"": ""https://instagram.ftxl1-1.fna.fbcdn.net/t51.2885-19/11906329_960233084022564_1448528159_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": true,
			""follower_count"": 2,
			""byline"": ""2 followers"",
			""mutual_followers_count"": 0.0
		}, {
			""pk"": 5867597536,
			""username"": ""codzienny_suchar_elo230"",
			""full_name"": ""myszka miki"",
			""is_private"": false,
			""profile_pic_url"": ""https://instagram.ftxl1-1.fna.fbcdn.net/t51.2885-19/11906329_960233084022564_1448528159_a.jpg"",
			""friendship_status"": {
				""following"": false,
				""is_private"": false,
				""incoming_request"": false,
				""outgoing_request"": false,
				""is_bestie"": false
			},
			""is_verified"": false,
			""has_anonymous_profile_picture"": true,
			""follower_count"": 1,
			""byline"": ""1 follower"",
			""mutual_followers_count"": 0.0
		}
	],
	""has_more"": false,
	""rank_token"": ""3b0955a3-c0b1-403a-942f-8965d0e96eaf"",
	""status"": ""ok""
}";

        [Theory]
        [InlineData("codziennysuchar")]
        [InlineData("codzienny_suchar")]
        [InlineData("codzienny.suchar")]
        public void CreateApiInstanceWithBuilder(string username)
        {
            var response = JsonConvert.DeserializeObject<InstaSearchUserResponse>(testJson);
            var user = response.Users?.FirstOrDefault(u => u.UserName == username);
            var fabric = ConvertersHelper.GetDefaultFabric();
            var converter = fabric.GetUserConverter(user);
            var result = converter.Convert();
            Assert.NotNull(result);
        }
    }
}