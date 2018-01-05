using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Helpers;
using Newtonsoft.Json;
using Xunit;

namespace InstaSharper.Tests.Infrastructure
{
    [Trait("Category", "Infrastructure")]
    public class StoryReelFeedTest
    {
        private const string testJson = @"{
	""tray"": [{
		""id"": 959317915,
		""latest_reel_media"": 1510854409,
		""expiring_at"": 1510940809,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 959317915,
			""username"": ""_.hadifaraji._"",
			""full_name"": ""\u202d10110010011010110101\u202c"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/23348221_112308092876279_8403103806681776128_n.jpg"",
			""profile_pic_id"": ""1644487357511490058_959317915"",
			""is_verified"": false
		},
		""ranked_position"": 1,
		""seen_ranked_position"": 1,
		""muted"": false,
		""prefetch_count"": 1,
		""has_besties_media"": false,
		""items"": [{
			""taken_at"": 1510854409,
			""pk"": 1649488959569722177,
			""id"": ""1649488959569722177_959317915"",
			""device_timestamp"": 1510854409,
			""media_type"": 1,
			""code"": ""BbkKXgoFPtB"",
			""client_cache_key"": ""MTY0OTQ4ODk1OTU2OTcyMjE3Nw==.2"",
			""filter_type"": 0,
			""image_versions2"": {
				""candidates"": [{
					""width"": 390,
					""height"": 640,
					""url"": ""https://scontent-cdg2-1.cdninstagram.com/vp/5b80afebea64bd895a658199f19f9869/5A1066B8/t58.9792-15/e35/12852417_1521254644618497_6599033683401768960_n.jpg?ig_cache_key=MTY0OTQ4ODk1OTU2OTcyMjE3Nw%3D%3D.2""
				},
				{
					""width"": 240,
					""height"": 393,
					""url"": ""https://scontent-cdg2-1.cdninstagram.com/vp/933128cc05a9be5c251f4c902173bc1a/5A108FFD/t58.9792-15/e35/p240x240/12852417_1521254644618497_6599033683401768960_n.jpg?ig_cache_key=MTY0OTQ4ODk1OTU2OTcyMjE3Nw%3D%3D.2""
				}]
			},
			""original_width"": 390,
			""original_height"": 640,
			""caption_position"": 0.0,
			""is_reel_media"": true,
			""user"": {
				""pk"": 959317915,
				""username"": ""_.hadifaraji._"",
				""full_name"": ""\u202d10110010011010110101\u202c"",
				""is_private"": false,
				""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/23348221_112308092876279_8403103806681776128_n.jpg"",
				""profile_pic_id"": ""1644487357511490058_959317915"",
				""is_verified"": false,
				""has_anonymous_profile_picture"": false,
				""is_unpublished"": false,
				""is_favorite"": false
			},
			""caption"": null,
			""caption_is_edited"": false,
			""photo_of_you"": false,
			""can_viewer_save"": true,
			""organic_tracking_token"": ""eyJ2ZXJzaW9uIjo1LCJwYXlsb2FkIjp7ImlzX2FuYWx5dGljc190cmFja2VkIjpmYWxzZSwidXVpZCI6IjVjOTRjZWY5MWFlYjQ2MDM5MzVhYWQzMTk0NjYzMjYzMTY0OTQ4ODk1OTU2OTcyMjE3NyIsInNlcnZlcl90b2tlbiI6IjE1MTA5MTczMTI0NjN8MTY0OTQ4ODk1OTU2OTcyMjE3N3wxNjQ3NzE4NDMyfDcwMjdmY2EyM2NhOTZkMmEzOTFiZGY4NWNhMzhkZmZjYmVlOGM0MTdkMGEzY2VkNzc3YTBmN2FkZWI5MjIxNTgifSwic2lnbmF0dXJlIjoiIn0="",
			""expiring_at"": 1510940809,
			""reel_mentions"": [],
			""story_locations"": [],
			""story_events"": [],
			""story_hashtags"": [],
			""story_polls"": [],
			""story_feed_media"": [],
			""can_reshare"": true,
			""supports_reel_reactions"": false
		}]
	},
	{
		""id"": 1391943577,
		""latest_reel_media"": 1510906144,
		""expiring_at"": 1510992544,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 1391943577,
			""username"": ""m.tiger"",
			""full_name"": ""TIGER"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/19050833_1879222825660819_7733253637681446912_a.jpg"",
			""profile_pic_id"": ""1534651095636688138_1391943577"",
			""is_verified"": false
		},
		""ranked_position"": 2,
		""seen_ranked_position"": 2,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false,
		""items"": [{
			""taken_at"": 1510906144,
			""pk"": 1649922808552366367,
			""id"": ""1649922808552366367_1391943577"",
			""device_timestamp"": 1510906144517,
			""media_type"": 1,
			""code"": ""BbltA1-lCEf"",
			""client_cache_key"": ""MTY0OTkyMjgwODU1MjM2NjM2Nw==.2"",
			""filter_type"": 0,
			""image_versions2"": {
				""candidates"": [{
					""width"": 1080,
					""height"": 1776,
					""url"": ""https://scontent-cdg2-1.cdninstagram.com/vp/f977e87ccecae5dbe607a57314dcc39d/5A1060C1/t58.9792-15/e35/16994222_160272154575559_6605841983955009536_n.jpg?se=7\u0026ig_cache_key=MTY0OTkyMjgwODU1MjM2NjM2Nw%3D%3D.2""
				},
				{
					""width"": 240,
					""height"": 394,
					""url"": ""https://scontent-cdg2-1.cdninstagram.com/vp/bcf53deeb7abb6a0043f558f2b607a64/5A105032/t58.9792-15/e35/p240x240/16994222_160272154575559_6605841983955009536_n.jpg?ig_cache_key=MTY0OTkyMjgwODU1MjM2NjM2Nw%3D%3D.2""
				}]
			},
			""original_width"": 1080,
			""original_height"": 1776,
			""caption_position"": 0.0,
			""is_reel_media"": true,
			""user"": {
				""pk"": 1391943577,
				""username"": ""m.tiger"",
				""full_name"": ""TIGER"",
				""is_private"": false,
				""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/19050833_1879222825660819_7733253637681446912_a.jpg"",
				""profile_pic_id"": ""1534651095636688138_1391943577"",
				""is_verified"": false,
				""has_anonymous_profile_picture"": false,
				""is_unpublished"": false,
				""is_favorite"": false
			},
			""caption"": null,
			""caption_is_edited"": false,
			""photo_of_you"": false,
			""can_viewer_save"": true,
			""organic_tracking_token"": ""eyJ2ZXJzaW9uIjo1LCJwYXlsb2FkIjp7ImlzX2FuYWx5dGljc190cmFja2VkIjpmYWxzZSwidXVpZCI6IjVjOTRjZWY5MWFlYjQ2MDM5MzVhYWQzMTk0NjYzMjYzMTY0OTkyMjgwODU1MjM2NjM2NyIsInNlcnZlcl90b2tlbiI6IjE1MTA5MTczMTI0NjN8MTY0OTkyMjgwODU1MjM2NjM2N3wxNjQ3NzE4NDMyfDUwNjQxZDE5ODEwNGEwYzI4ZDEwMmE0NjdhYWM5MDU5ODg4MmNmYTUzZjdmYzMwNzFiMGVkNjA4ZTZmNWY3MzQifSwic2lnbmF0dXJlIjoiIn0="",
			""expiring_at"": 1510992544,
			""reel_mentions"": [],
			""story_locations"": [],
			""story_events"": [],
			""story_hashtags"": [],
			""story_polls"": [],
			""story_feed_media"": [],
			""can_reshare"": true,
			""supports_reel_reactions"": false
		}]
	},
	{
		""id"": 3206926093,
		""latest_reel_media"": 1510908005,
		""expiring_at"": 1510994405,
		""seen"": 0.0,
		""can_reply"": false,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 3206926093,
			""username"": ""ho3in0272"",
			""full_name"": ""Ho3in"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/17817785_456510021347810_7510459570775392256_a.jpg"",
			""profile_pic_id"": ""1487521582481690239_3206926093"",
			""is_verified"": false
		},
		""ranked_position"": 3,
		""seen_ranked_position"": 3,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false,
		""items"": [{
			""taken_at"": 1510908005,
			""pk"": 1649938507261317983,
			""id"": ""1649938507261317983_3206926093"",
			""device_timestamp"": 1510908005432,
			""media_type"": 1,
			""code"": ""BblwlSijJNf"",
			""client_cache_key"": ""MTY0OTkzODUwNzI2MTMxNzk4Mw==.2"",
			""filter_type"": 0,
			""image_versions2"": {
				""candidates"": [{
					""width"": 540,
					""height"": 960,
					""url"": ""https://scontent-cdg2-1.cdninstagram.com/vp/742a4dd0ccecdf63b33f2d76c9cfb19a/5A102C1B/t58.9792-15/e35/20775352_130201331028706_1523758292432584704_n.jpg?ig_cache_key=MTY0OTkzODUwNzI2MTMxNzk4Mw%3D%3D.2""
				},
				{
					""width"": 240,
					""height"": 426,
					""url"": ""https://scontent-cdg2-1.cdninstagram.com/vp/d5bca24e2f10ff54e883dbf9a353a49b/5A102D88/t58.9792-15/e35/p240x240/20775352_130201331028706_1523758292432584704_n.jpg?ig_cache_key=MTY0OTkzODUwNzI2MTMxNzk4Mw%3D%3D.2""
				}]
			},
			""original_width"": 540,
			""original_height"": 960,
			""caption_position"": 0.0,
			""is_reel_media"": true,
			""user"": {
				""pk"": 3206926093,
				""username"": ""ho3in0272"",
				""full_name"": ""Ho3in"",
				""is_private"": false,
				""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/17817785_456510021347810_7510459570775392256_a.jpg"",
				""profile_pic_id"": ""1487521582481690239_3206926093"",
				""is_verified"": false,
				""has_anonymous_profile_picture"": false,
				""is_unpublished"": false,
				""is_favorite"": false
			},
			""caption"": null,
			""caption_is_edited"": false,
			""photo_of_you"": false,
			""can_viewer_save"": true,
			""organic_tracking_token"": ""eyJ2ZXJzaW9uIjo1LCJwYXlsb2FkIjp7ImlzX2FuYWx5dGljc190cmFja2VkIjpmYWxzZSwidXVpZCI6IjVjOTRjZWY5MWFlYjQ2MDM5MzVhYWQzMTk0NjYzMjYzMTY0OTkzODUwNzI2MTMxNzk4MyIsInNlcnZlcl90b2tlbiI6IjE1MTA5MTczMTI0NjN8MTY0OTkzODUwNzI2MTMxNzk4M3wxNjQ3NzE4NDMyfDVkMTU0MTNhODc4ZGY5ZWMyYmMxZWQwYmU0MWYyMTc5NjJmYzFjMzFlM2M2YjAzZjgxYzNhZDM0YTgxMjkzZjAifSwic2lnbmF0dXJlIjoiIn0="",
			""expiring_at"": 1510994405,
			""imported_taken_at"": 1510907979,
			""reel_mentions"": [],
			""story_locations"": [],
			""story_events"": [],
			""story_hashtags"": [],
			""story_polls"": [],
			""story_feed_media"": [],
			""can_reshare"": true,
			""supports_reel_reactions"": false
		}]
	},
	{
		""id"": 2057461165,
		""latest_reel_media"": 1510862508,
		""expiring_at"": 1510948908,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 2057461165,
			""username"": ""ayria.b"",
			""full_name"": ""ayria"",
			""is_private"": true,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/22157464_122412968458824_5056550025048358912_n.jpg"",
			""profile_pic_id"": ""1618215817073442624_2057461165"",
			""is_verified"": false
		},
		""ranked_position"": 4,
		""seen_ranked_position"": 4,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false,
		""items"": [{
			""taken_at"": 1510862378,
			""pk"": 1649556176310660131,
			""id"": ""1649556176310660131_2057461165"",
			""device_timestamp"": 373165517843589,
			""media_type"": 1,
			""code"": ""BbkZppGDGwjoXRukhmmEIU0EAw4nI-qPUuHc780"",
			""client_cache_key"": ""MTY0OTU1NjE3NjMxMDY2MDEzMQ==.2"",
			""filter_type"": 0,
			""image_versions2"": {
				""candidates"": [{
					""width"": 1080,
					""height"": 1920,
					""url"": ""https://scontent-cdg2-1.cdninstagram.com/vp/31cffb90989f0ebc1027f1d7cf3dd0a0/5A105D39/t58.9792-15/e35/17568037_129576284428483_7628768821515386880_n.jpg?se=7\u0026ig_cache_key=MTY0OTU1NjE3NjMxMDY2MDEzMQ%3D%3D.2""
				},
				{
					""width"": 240,
					""height"": 426,
					""url"": ""https://scontent-cdg2-1.cdninstagram.com/vp/41c19e515b51c92502412ca69ef1519c/5A105322/t58.9792-15/e35/p240x240/17568037_129576284428483_7628768821515386880_n.jpg?ig_cache_key=MTY0OTU1NjE3NjMxMDY2MDEzMQ%3D%3D.2""
				}]
			},
			""original_width"": 1080,
			""original_height"": 1920,
			""caption_position"": 0.0,
			""is_reel_media"": true,
			""user"": {
				""pk"": 2057461165,
				""username"": ""ayria.b"",
				""full_name"": ""ayria"",
				""is_private"": true,
				""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/22157464_122412968458824_5056550025048358912_n.jpg"",
				""profile_pic_id"": ""1618215817073442624_2057461165"",
				""is_verified"": false,
				""has_anonymous_profile_picture"": false,
				""is_unpublished"": false,
				""is_favorite"": false
			},
			""caption"": null,
			""caption_is_edited"": false,
			""photo_of_you"": false,
			""can_viewer_save"": true,
			""organic_tracking_token"": ""eyJ2ZXJzaW9uIjo1LCJwYXlsb2FkIjp7ImlzX2FuYWx5dGljc190cmFja2VkIjpmYWxzZSwidXVpZCI6IjVjOTRjZWY5MWFlYjQ2MDM5MzVhYWQzMTk0NjYzMjYzMTY0OTU1NjE3NjMxMDY2MDEzMSIsInNlcnZlcl90b2tlbiI6IjE1MTA5MTczMTI0NjN8MTY0OTU1NjE3NjMxMDY2MDEzMXwxNjQ3NzE4NDMyfDY5NTI1MGVkN2NhMWNmNWMyNWEwZGMxZTRmOTg4Y2MyZTVlYTIyNTc5N2I0OWE1NDNiZTZhOWUwM2QzOGQyZDQifSwic2lnbmF0dXJlIjoiIn0="",
			""expiring_at"": 1510948778,
			""imported_taken_at"": 1510862091,
			""reel_mentions"": [],
			""story_locations"": [],
			""story_events"": [],
			""story_hashtags"": [],
			""story_polls"": [],
			""story_feed_media"": [],
			""can_reshare"": true,
			""supports_reel_reactions"": false
		},
		{
			""taken_at"": 1510862508,
			""pk"": 1649556777279099195,
			""id"": ""1649556777279099195_2057461165"",
			""device_timestamp"": 373296285005034,
			""media_type"": 1,
			""code"": ""BbkZyYyjxE7yF9PECklEfkGDt6C3kodZ2Ss0Nc0"",
			""client_cache_key"": ""MTY0OTU1Njc3NzI3OTA5OTE5NQ==.2"",
			""filter_type"": 0,
			""image_versions2"": {
				""candidates"": [{
					""width"": 1080,
					""height"": 1920,
					""url"": ""https://scontent-cdg2-1.cdninstagram.com/vp/c70b71f1afd04a7016a0b06fe02af7a6/5A103CD4/t58.9792-15/e35/18448471_189954088227361_2974203919778971648_n.jpg?se=7\u0026ig_cache_key=MTY0OTU1Njc3NzI3OTA5OTE5NQ%3D%3D.2""
				},
				{
					""width"": 240,
					""height"": 426,
					""url"": ""https://scontent-cdg2-1.cdninstagram.com/vp/4d22109912171cdd294f38faaa0cc0d0/5A1081C3/t58.9792-15/e35/p240x240/18448471_189954088227361_2974203919778971648_n.jpg?ig_cache_key=MTY0OTU1Njc3NzI3OTA5OTE5NQ%3D%3D.2""
				}]
			},
			""original_width"": 1080,
			""original_height"": 1920,
			""caption_position"": 0.0,
			""is_reel_media"": true,
			""user"": {
				""pk"": 2057461165,
				""username"": ""ayria.b"",
				""full_name"": ""ayria"",
				""is_private"": true,
				""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/22157464_122412968458824_5056550025048358912_n.jpg"",
				""profile_pic_id"": ""1618215817073442624_2057461165"",
				""is_verified"": false,
				""has_anonymous_profile_picture"": false,
				""is_unpublished"": false,
				""is_favorite"": false
			},
			""caption"": null,
			""caption_is_edited"": false,
			""photo_of_you"": false,
			""can_viewer_save"": true,
			""organic_tracking_token"": ""eyJ2ZXJzaW9uIjo1LCJwYXlsb2FkIjp7ImlzX2FuYWx5dGljc190cmFja2VkIjpmYWxzZSwidXVpZCI6IjVjOTRjZWY5MWFlYjQ2MDM5MzVhYWQzMTk0NjYzMjYzMTY0OTU1Njc3NzI3OTA5OTE5NSIsInNlcnZlcl90b2tlbiI6IjE1MTA5MTczMTI0NjN8MTY0OTU1Njc3NzI3OTA5OTE5NXwxNjQ3NzE4NDMyfDE4MmFmNTE5YWQ5ZmFhMjlmM2Q1ODliMjA0MThhZDRkYmJkNjcwM2IwMWMwOTY5N2Q0MGI4MGFiZjIzZmMwNmMifSwic2lnbmF0dXJlIjoiIn0="",
			""expiring_at"": 1510948908,
			""imported_taken_at"": 1510845400,
			""reel_mentions"": [],
			""story_locations"": [],
			""story_events"": [],
			""story_hashtags"": [],
			""story_polls"": [],
			""story_feed_media"": [],
			""can_reshare"": true,
			""supports_reel_reactions"": false
		}]
	},
	{
		""id"": 1489869781,
		""latest_reel_media"": 1510903812,
		""expiring_at"": 1510990212,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 1489869781,
			""username"": ""ali_akbari_1994"",
			""full_name"": ""Ali Akbari"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/11008223_1642746932642809_2053218239_a.jpg"",
			""is_verified"": false
		},
		""ranked_position"": 5,
		""seen_ranked_position"": 5,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 5482540873,
		""latest_reel_media"": 1510900559,
		""expiring_at"": 1510986959,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 5482540873,
			""username"": ""_kimixe"",
			""full_name"": ""\u269c\u06a9\u0640\u06cc\u0645\u06cc\u0640\u0627 \u062f\u0631\u064e\u062e\u0640\u0640\u0640\u0634\u0627\u0646\u06cc\ud83d\udc78\ud83c\udffb"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/21436207_272984799873060_3437969849836371968_a.jpg"",
			""profile_pic_id"": ""1600899676696408725_5482540873"",
			""is_verified"": false
		},
		""ranked_position"": 6,
		""seen_ranked_position"": 6,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 3066946863,
		""latest_reel_media"": 1510831151,
		""expiring_at"": 1510917551,
		""seen"": 0.0,
		""can_reply"": false,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 3066946863,
			""username"": ""video__tanz"",
			""full_name"": ""\ud83d\ude02 Video Tanz \ud83d\ude02"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/23279217_302064020275335_2450216195975020544_n.jpg"",
			""profile_pic_id"": ""1489831773834723134_3066946863"",
			""is_verified"": false
		},
		""ranked_position"": 7,
		""seen_ranked_position"": 7,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 251137241,
		""latest_reel_media"": 1510847715,
		""expiring_at"": 1510934115,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 251137241,
			""username"": ""svetabily"",
			""full_name"": ""Sveta Bilyalova"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/20398229_871582456329183_4794696546299936768_a.jpg"",
			""profile_pic_id"": ""1566324341121474832_251137241"",
			""is_verified"": true
		},
		""ranked_position"": 8,
		""seen_ranked_position"": 8,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 3405890655,
		""latest_reel_media"": 1510914372,
		""expiring_at"": 1511000772,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 3405890655,
			""username"": ""saburi_ali"",
			""full_name"": ""\u2604Ali\u26a1\ufe0fSaburi\ud83d\udd38"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/22637548_764050613801850_6552322718602100736_n.jpg"",
			""profile_pic_id"": ""1629689145662538438_3405890655"",
			""is_verified"": false
		},
		""ranked_position"": 9,
		""seen_ranked_position"": 9,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 3551724865,
		""latest_reel_media"": 1510895779,
		""expiring_at"": 1510982179,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": false,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 3551724865,
			""username"": ""mehdi_shtoori"",
			""full_name"": ""mehdi_shatoori"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/23594874_186693181881005_6648547247506063360_n.jpg"",
			""profile_pic_id"": ""1649578282054939172_3551724865"",
			""is_verified"": false
		},
		""ranked_position"": 10,
		""seen_ranked_position"": 10,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 38334017,
		""latest_reel_media"": 1510852186,
		""expiring_at"": 1510938586,
		""seen"": 0.0,
		""can_reply"": false,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 38334017,
			""username"": ""tohi"",
			""full_name"": ""Tohi"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/22157401_1164828236984070_3300847736101797888_n.jpg"",
			""profile_pic_id"": ""1617773928800265440_38334017"",
			""is_verified"": true
		},
		""ranked_position"": 11,
		""seen_ranked_position"": 11,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 271236276,
		""latest_reel_media"": 1510832936,
		""expiring_at"": 1510919336,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 271236276,
			""username"": ""donyadadrasan"",
			""full_name"": ""\u062f\u0646\u06cc\u0627"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/23498668_1768894090082015_6493720728821563392_n.jpg"",
			""profile_pic_id"": ""1645538026368962501_271236276"",
			""is_verified"": false
		},
		""ranked_position"": 12,
		""seen_ranked_position"": 12,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 1510966691,
		""latest_reel_media"": 1510916639,
		""expiring_at"": 1511003039,
		""seen"": 0.0,
		""can_reply"": false,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 1510966691,
			""username"": ""lisaandlena"",
			""full_name"": ""Lisa and Lena | Germany\u00ae"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/20184530_1714340778874426_8644379796467351552_a.jpg"",
			""profile_pic_id"": ""1564408562074097932_1510966691"",
			""is_verified"": true
		},
		""ranked_position"": 13,
		""seen_ranked_position"": 13,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 3282438449,
		""latest_reel_media"": 1510914206,
		""expiring_at"": 1511000606,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 3282438449,
			""username"": ""sepehr.k.a.80"",
			""full_name"": ""sepehr.k.a.80"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/20759440_133387480611580_3356761454312161280_a.jpg"",
			""profile_pic_id"": ""1578253411031495954_3282438449"",
			""is_verified"": false
		},
		""ranked_position"": 14,
		""seen_ranked_position"": 14,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 1561068104,
		""latest_reel_media"": 1510856008,
		""expiring_at"": 1510942408,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 1561068104,
			""username"": ""negin_xaniaristm"",
			""full_name"": ""\ud83d\udc8e\u06cc\u0647 \u0646\u06af\u06cc\u0640^\u2022^\u0640\u0646 \u062e\u0633\u0631\u0648\u06cc\u0633\u062a\u200c\u062a\u06cc\u200c\u0627\u0650\u0645\u06cc\ud83d\udc8e"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/23507807_128028924552207_1606758449528438784_n.jpg"",
			""profile_pic_id"": ""1646591608245492499_1561068104"",
			""is_verified"": false
		},
		""ranked_position"": 16,
		""seen_ranked_position"": 16,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 10245870,
		""latest_reel_media"": 1510907836,
		""expiring_at"": 1510994236,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 10245870,
			""username"": ""amandacerny"",
			""full_name"": ""Amanda Cerny"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/22637117_1696268193770897_1780284289452081152_n.jpg"",
			""profile_pic_id"": ""1628386071727763294_10245870"",
			""is_verified"": true
		},
		""ranked_position"": 17,
		""seen_ranked_position"": 17,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 2244850095,
		""latest_reel_media"": 1510871575,
		""expiring_at"": 1510957975,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 2244850095,
			""username"": ""funtv_ir"",
			""full_name"": ""FunTV \u272a \u0641\u0627\u0646 \u062a\u06cc \u0648\u06cc"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/21827728_905466152934893_4542388546467528704_n.jpg"",
			""profile_pic_id"": ""1568133572379051405_2244850095"",
			""is_verified"": false
		},
		""ranked_position"": 18,
		""seen_ranked_position"": 18,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 12299923,
		""latest_reel_media"": 1510877891,
		""expiring_at"": 1510964291,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 12299923,
			""username"": ""andreaespadatv"",
			""full_name"": ""\u15e9\u144e\u15ea\u1587E\u15e9 E\u1515\u146d\u15e9\u15ea\u15e9"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/20905332_1992303564377138_6941782205450420224_a.jpg"",
			""profile_pic_id"": ""1585831165661932658_12299923"",
			""is_verified"": true
		},
		""ranked_position"": 19,
		""seen_ranked_position"": 19,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 1646749288,
		""latest_reel_media"": 1510906828,
		""expiring_at"": 1510993228,
		""seen"": 0.0,
		""can_reply"": false,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 1646749288,
			""username"": ""jazabtarin_cliphaa"",
			""full_name"": ""\ud83d\udd4b\u0628\u0633\u0645 \u0627\u0644\u0644\u0647 \u0627\u0644\u0631\u062d\u0645\u0646 \u0627\u0644\u0631\u062d\u06cc\u0645\ud83d\udd4b"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/23347372_180675915816021_4310249219135897600_n.jpg"",
			""profile_pic_id"": ""1501663558492258008_1646749288"",
			""is_verified"": false
		},
		""ranked_position"": 20,
		""seen_ranked_position"": 20,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 1259128889,
		""latest_reel_media"": 1510904116,
		""expiring_at"": 1510990516,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 1259128889,
			""username"": ""clipp_video"",
			""full_name"": ""\u06a9\u0644\u06cc\u067e \u0648\u06cc\u062f\u0626\u0648(\u062e\u0646\u062f\u0647 \u0641\u0627\u0646 \u0633\u0631\u06af\u0631\u0645\u06cc )"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/22429821_148439145763324_7717555759847833600_n.jpg"",
			""profile_pic_id"": ""1308169938154726997_1259128889"",
			""is_verified"": false
		},
		""ranked_position"": 21,
		""seen_ranked_position"": 21,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 442625,
		""latest_reel_media"": 1510890967,
		""expiring_at"": 1510977367,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 442625,
			""username"": ""journeydan"",
			""full_name"": ""Daniel Bader"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/11348095_814564621974849_480326611_a.jpg"",
			""is_verified"": false
		},
		""ranked_position"": 22,
		""seen_ranked_position"": 22,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 2437674150,
		""latest_reel_media"": 1510862618,
		""expiring_at"": 1510949018,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 2437674150,
			""username"": ""dubsmash_clip_ir"",
			""full_name"": ""\u062f\u0627\u0628\u0633\u0645\u0634_\u06a9\u0644\u06cc\u067e \u0627\u06cc\u0631\u0627\u0646\u06cc"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/23507577_1989357354677188_6071511854348238848_n.jpg"",
			""profile_pic_id"": ""1565995338656106798_2437674150"",
			""is_verified"": false
		},
		""ranked_position"": 23,
		""seen_ranked_position"": 23,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 1451667254,
		""latest_reel_media"": 1510907597,
		""expiring_at"": 1510993997,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 1451667254,
			""username"": ""ir___photographer"",
			""full_name"": ""Photography Ideas"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/20759619_110898839576437_1401961498183467008_a.jpg"",
			""profile_pic_id"": ""1579067129738486356_1451667254"",
			""is_verified"": false
		},
		""ranked_position"": 24,
		""seen_ranked_position"": 24,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	},
	{
		""id"": 1548541077,
		""latest_reel_media"": 1510845566,
		""expiring_at"": 1510931966,
		""seen"": 0.0,
		""can_reply"": true,
		""can_reshare"": true,
		""reel_type"": ""user_reel"",
		""user"": {
			""pk"": 1548541077,
			""username"": ""hanjare_talaei"",
			""full_name"": ""hanjare talaei \ud83c\udf99\u062d\u0646\u062c\u0631\u0647 \u0637\u0644\u0627\u064a\u0649"",
			""is_private"": false,
			""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/23101843_1849658215074801_563374214585778176_n.jpg"",
			""profile_pic_id"": ""1640555686213844652_1548541077"",
			""is_verified"": false
		},
		""ranked_position"": 25,
		""seen_ranked_position"": 25,
		""muted"": false,
		""prefetch_count"": 0,
		""has_besties_media"": false
	}],
	""post_live"": {
		""post_live_items"": [{
			""pk"": ""post_live_1383543459"",
			""user"": {
				""pk"": 1383543459,
				""username"": ""780ir"",
				""full_name"": ""\u0647\u0641\u0640 \u0647\u0634\u062a\u0627\u062f | #\u0667\u0668\u0660*"",
				""is_private"": false,
				""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/21980259_360063074444115_3710275616830914560_n.jpg"",
				""profile_pic_id"": ""1532382334209046138_1383543459"",
				""is_verified"": false
			},
			""broadcasts"": [{
				""id"": 17908970164059694,
				""broadcast_status"": ""post_live"",
				""dash_manifest"": ""\u003cMPD xmlns=\""urn:mpeg:dash:schema:mpd:2011\"" minBufferTime=\""PT1.500S\"" type=\""static\"" mediaPresentationDuration=\""PT0H16M24.622S\"" maxSegmentDuration=\""PT0H0M2.000S\"" profiles=\""urn:mpeg:dash:profile:isoff-on-demand:2011,http://dashif.org/guidelines/dash264\""\u003e\u003cPeriod duration=\""PT0H16M24.622S\""\u003e\u003cAdaptationSet segmentAlignment=\""true\"" maxWidth=\""396\"" maxHeight=\""704\"" maxFrameRate=\""16000/544\"" par=\""396:704\"" lang=\""und\"" subsegmentAlignment=\""true\"" subsegmentStartsWithSAP=\""1\""\u003e\u003cRepresentation id=\""17895622549102837v\"" mimeType=\""video/mp4\"" codecs=\""avc1.4d401f\"" width=\""396\"" height=\""704\"" frameRate=\""16000/544\"" sar=\""1:1\"" startWithSAP=\""1\"" bandwidth=\""582064\"" FBQualityClass=\""sd\"" FBQualityLabel=\""396w\""\u003e\u003cBaseURL\u003ehttps://scontent-cdg2-1.cdninstagram.com/t72.12950-16/10000000_1740452772915569_6670090197872410624_n.mp4\u003c/BaseURL\u003e\u003cSegmentBase indexRangeExact=\""true\"" indexRange=\""899-6930\""\u003e\u003cInitialization range=\""0-898\""/\u003e\u003c/SegmentBase\u003e\u003c/Representation\u003e\u003c/AdaptationSet\u003e\u003cAdaptationSet segmentAlignment=\""true\"" lang=\""und\"" subsegmentAlignment=\""true\"" subsegmentStartsWithSAP=\""1\""\u003e\u003cRepresentation id=\""17895622549102837a\"" mimeType=\""audio/mp4\"" codecs=\""mp4a.40.2\"" audioSamplingRate=\""44100\"" startWithSAP=\""1\"" bandwidth=\""50283\""\u003e\u003cAudioChannelConfiguration schemeIdUri=\""urn:mpeg:dash:23003:3:audio_channel_configuration:2011\"" value=\""2\""/\u003e\u003cBaseURL\u003ehttps://scontent-cdg2-1.cdninstagram.com/t72.12950-16/23586292_132647884105636_1354989047184883712_n.mp4\u003c/BaseURL\u003e\u003cSegmentBase indexRangeExact=\""true\"" indexRange=\""835-6794\""\u003e\u003cInitialization range=\""0-834\""/\u003e\u003c/SegmentBase\u003e\u003c/Representation\u003e\u003c/AdaptationSet\u003e\u003c/Period\u003e\u003c/MPD\u003e"",
				""expire_at"": 1510939433,
				""encoding_tag"": ""instagram_dash_remuxed"",
				""internal_only"": false,
				""number_of_qualities"": 1,
				""cover_frame_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.9792-15/18979052_757168924407392_3730758058167500800_n.jpg"",
				""broadcast_owner"": {
					""pk"": 1383543459,
					""username"": ""780ir"",
					""full_name"": ""\u0647\u0641\u0640 \u0647\u0634\u062a\u0627\u062f | #\u0667\u0668\u0660*"",
					""is_private"": false,
					""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/21980259_360063074444115_3710275616830914560_n.jpg"",
					""profile_pic_id"": ""1532382334209046138_1383543459"",
					""friendship_status"": {
						""following"": true,
						""followed_by"": false,
						""blocking"": false,
						""is_private"": false,
						""incoming_request"": false,
						""outgoing_request"": false,
						""is_bestie"": false
					},
					""is_verified"": false
				},
				""published_time"": 1510852005,
				""media_id"": ""1649468551528436844_1383543459"",
				""broadcast_message"": """",
				""organic_tracking_token"": ""eyJ2ZXJzaW9uIjo1LCJwYXlsb2FkIjp7ImlzX2FuYWx5dGljc190cmFja2VkIjp0cnVlLCJ1dWlkIjoiNWM5NGNlZjkxYWViNDYwMzkzNWFhZDMxOTQ2NjMyNjMxNjQ5NDY4NTUxNTI4NDM2ODQ0Iiwic2VydmVyX3Rva2VuIjoiMTUxMDkxNzMxMjM2OXwxNjQ5NDY4NTUxNTI4NDM2ODQ0fDE2NDc3MTg0MzJ8NjM2ODAyZmVjMzA5MDBjN2YzZDY3ZWQzZTgxODliYmM5M2ViZWI4MTFiYzIwNzdhMzM2ZjMxMDI4ZjIzYWJlMiJ9LCJzaWduYXR1cmUiOiIifQ==""
			},
			{
				""id"": 17883870826134571,
				""broadcast_status"": ""post_live"",
				""dash_manifest"": ""\u003cMPD xmlns=\""urn:mpeg:dash:schema:mpd:2011\"" minBufferTime=\""PT1.500S\"" type=\""static\"" mediaPresentationDuration=\""PT0H14M6.835S\"" maxSegmentDuration=\""PT0H0M2.000S\"" profiles=\""urn:mpeg:dash:profile:isoff-on-demand:2011,http://dashif.org/guidelines/dash264\""\u003e\u003cPeriod duration=\""PT0H14M6.835S\""\u003e\u003cAdaptationSet segmentAlignment=\""true\"" maxWidth=\""396\"" maxHeight=\""704\"" maxFrameRate=\""16000/528\"" par=\""396:704\"" lang=\""und\"" subsegmentAlignment=\""true\"" subsegmentStartsWithSAP=\""1\""\u003e\u003cRepresentation id=\""17892232666080922v\"" mimeType=\""video/mp4\"" codecs=\""avc1.4d401f\"" width=\""396\"" height=\""704\"" frameRate=\""16000/528\"" sar=\""1:1\"" startWithSAP=\""1\"" bandwidth=\""537007\"" FBQualityClass=\""sd\"" FBQualityLabel=\""396w\""\u003e\u003cBaseURL\u003ehttps://scontent-cdg2-1.cdninstagram.com/t72.12950-16/10000000_170347730220671_9006027248360226816_n.mp4\u003c/BaseURL\u003e\u003cSegmentBase indexRangeExact=\""true\"" indexRange=\""899-6054\""\u003e\u003cInitialization range=\""0-898\""/\u003e\u003c/SegmentBase\u003e\u003c/Representation\u003e\u003c/AdaptationSet\u003e\u003cAdaptationSet segmentAlignment=\""true\"" lang=\""und\"" subsegmentAlignment=\""true\"" subsegmentStartsWithSAP=\""1\""\u003e\u003cRepresentation id=\""17892232666080922a\"" mimeType=\""audio/mp4\"" codecs=\""mp4a.40.2\"" audioSamplingRate=\""44100\"" startWithSAP=\""1\"" bandwidth=\""50316\""\u003e\u003cAudioChannelConfiguration schemeIdUri=\""urn:mpeg:dash:23003:3:audio_channel_configuration:2011\"" value=\""2\""/\u003e\u003cBaseURL\u003ehttps://scontent-cdg2-1.cdninstagram.com/t72.12950-16/18743538_2044045792491235_3540743782959939584_n.mp4\u003c/BaseURL\u003e\u003cSegmentBase indexRangeExact=\""true\"" indexRange=\""835-5966\""\u003e\u003cInitialization range=\""0-834\""/\u003e\u003c/SegmentBase\u003e\u003c/Representation\u003e\u003c/AdaptationSet\u003e\u003c/Period\u003e\u003c/MPD\u003e"",
				""expire_at"": 1510945469,
				""encoding_tag"": ""instagram_dash_remuxed"",
				""internal_only"": false,
				""number_of_qualities"": 1,
				""cover_frame_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.9792-15/16751331_1936086796654040_1708202798216118272_n.jpg"",
				""broadcast_owner"": {
					""pk"": 1383543459,
					""username"": ""780ir"",
					""full_name"": ""\u0647\u0641\u0640 \u0647\u0634\u062a\u0627\u062f | #\u0667\u0668\u0660*"",
					""is_private"": false,
					""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/21980259_360063074444115_3710275616830914560_n.jpg"",
					""profile_pic_id"": ""1532382334209046138_1383543459"",
					""friendship_status"": {
						""following"": true,
						""followed_by"": false,
						""blocking"": false,
						""is_private"": false,
						""incoming_request"": false,
						""outgoing_request"": false,
						""is_bestie"": false
					},
					""is_verified"": false
				},
				""published_time"": 1510858155,
				""media_id"": ""1649520175491644674_1383543459"",
				""broadcast_message"": """",
				""organic_tracking_token"": ""eyJ2ZXJzaW9uIjo1LCJwYXlsb2FkIjp7ImlzX2FuYWx5dGljc190cmFja2VkIjp0cnVlLCJ1dWlkIjoiNWM5NGNlZjkxYWViNDYwMzkzNWFhZDMxOTQ2NjMyNjMxNjQ5NTIwMTc1NDkxNjQ0Njc0Iiwic2VydmVyX3Rva2VuIjoiMTUxMDkxNzMxMjM3NnwxNjQ5NTIwMTc1NDkxNjQ0Njc0fDE2NDc3MTg0MzJ8MGE5OTlhNWY1NjM2ZDM2ZmMwNWYwZDFhYWNiMGUzY2QyNzhlOTY1NGE4YjY4MmQ0YzMwMGIyZWNkY2U0MDFmMiJ9LCJzaWduYXR1cmUiOiIifQ==""
			},
			{
				""id"": 17881002145166791,
				""broadcast_status"": ""post_live"",
				""dash_manifest"": ""\u003cMPD xmlns=\""urn:mpeg:dash:schema:mpd:2011\"" minBufferTime=\""PT1.500S\"" type=\""static\"" mediaPresentationDuration=\""PT0H14M30.033S\"" maxSegmentDuration=\""PT0H0M2.000S\"" profiles=\""urn:mpeg:dash:profile:isoff-on-demand:2011,http://dashif.org/guidelines/dash264\""\u003e\u003cPeriod duration=\""PT0H14M30.033S\""\u003e\u003cAdaptationSet segmentAlignment=\""true\"" maxWidth=\""396\"" maxHeight=\""704\"" maxFrameRate=\""16000/544\"" par=\""396:704\"" lang=\""und\"" subsegmentAlignment=\""true\"" subsegmentStartsWithSAP=\""1\""\u003e\u003cRepresentation id=\""17908553083038374v\"" mimeType=\""video/mp4\"" codecs=\""avc1.4d401f\"" width=\""396\"" height=\""704\"" frameRate=\""16000/544\"" sar=\""1:1\"" startWithSAP=\""1\"" bandwidth=\""772699\"" FBQualityClass=\""sd\"" FBQualityLabel=\""396w\""\u003e\u003cBaseURL\u003ehttps://scontent-cdg2-1.cdninstagram.com/t72.12950-16/10000000_1950041318580288_3640943338456088576_n.mp4\u003c/BaseURL\u003e\u003cSegmentBase indexRangeExact=\""true\"" indexRange=\""899-6174\""\u003e\u003cInitialization range=\""0-898\""/\u003e\u003c/SegmentBase\u003e\u003c/Representation\u003e\u003c/AdaptationSet\u003e\u003cAdaptationSet segmentAlignment=\""true\"" lang=\""und\"" subsegmentAlignment=\""true\"" subsegmentStartsWithSAP=\""1\""\u003e\u003cRepresentation id=\""17908553083038374a\"" mimeType=\""audio/mp4\"" codecs=\""mp4a.40.2\"" audioSamplingRate=\""44100\"" startWithSAP=\""1\"" bandwidth=\""50308\""\u003e\u003cAudioChannelConfiguration schemeIdUri=\""urn:mpeg:dash:23003:3:audio_channel_configuration:2011\"" value=\""2\""/\u003e\u003cBaseURL\u003ehttps://scontent-cdg2-1.cdninstagram.com/t72.12950-16/19097229_134461557323949_4728992527447752704_n.mp4\u003c/BaseURL\u003e\u003cSegmentBase indexRangeExact=\""true\"" indexRange=\""835-6098\""\u003e\u003cInitialization range=\""0-834\""/\u003e\u003c/SegmentBase\u003e\u003c/Representation\u003e\u003c/AdaptationSet\u003e\u003c/Period\u003e\u003c/MPD\u003e"",
				""expire_at"": 1510920674,
				""encoding_tag"": ""instagram_dash_remuxed"",
				""internal_only"": false,
				""number_of_qualities"": 1,
				""cover_frame_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.9792-15/14964412_932613113564850_5738793949246521344_n.jpg"",
				""broadcast_owner"": {
					""pk"": 1383543459,
					""username"": ""780ir"",
					""full_name"": ""\u0647\u0641\u0640 \u0647\u0634\u062a\u0627\u062f | #\u0667\u0668\u0660*"",
					""is_private"": false,
					""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/21980259_360063074444115_3710275616830914560_n.jpg"",
					""profile_pic_id"": ""1532382334209046138_1383543459"",
					""friendship_status"": {
						""following"": true,
						""followed_by"": false,
						""blocking"": false,
						""is_private"": false,
						""incoming_request"": false,
						""outgoing_request"": false,
						""is_bestie"": false
					},
					""is_verified"": false
				},
				""published_time"": 1510833390,
				""media_id"": ""1649312402740902617_1383543459"",
				""broadcast_message"": """",
				""organic_tracking_token"": ""eyJ2ZXJzaW9uIjo1LCJwYXlsb2FkIjp7ImlzX2FuYWx5dGljc190cmFja2VkIjp0cnVlLCJ1dWlkIjoiNWM5NGNlZjkxYWViNDYwMzkzNWFhZDMxOTQ2NjMyNjMxNjQ5MzEyNDAyNzQwOTAyNjE3Iiwic2VydmVyX3Rva2VuIjoiMTUxMDkxNzMxMjM4NnwxNjQ5MzEyNDAyNzQwOTAyNjE3fDE2NDc3MTg0MzJ8ZmUwYWNiYjdiNmViOTViNWU3YThjYzVlNzMxMGQ5ZTY1ZTIzZWY0MWI2NTdmNWZkZDA5Yjg3NTQzMDI2MWY0ZCJ9LCJzaWduYXR1cmUiOiIifQ==""
			},
			{
				""id"": 17909222266016333,
				""broadcast_status"": ""post_live"",
				""dash_manifest"": ""\u003cMPD xmlns=\""urn:mpeg:dash:schema:mpd:2011\"" minBufferTime=\""PT1.500S\"" type=\""static\"" mediaPresentationDuration=\""PT0H8M20.366S\"" maxSegmentDuration=\""PT0H0M2.000S\"" profiles=\""urn:mpeg:dash:profile:isoff-on-demand:2011,http://dashif.org/guidelines/dash264\""\u003e\u003cPeriod duration=\""PT0H8M20.366S\""\u003e\u003cAdaptationSet segmentAlignment=\""true\"" maxWidth=\""396\"" maxHeight=\""704\"" maxFrameRate=\""16000/528\"" par=\""396:704\"" lang=\""und\"" subsegmentAlignment=\""true\"" subsegmentStartsWithSAP=\""1\""\u003e\u003cRepresentation id=\""17848696471221278v\"" mimeType=\""video/mp4\"" codecs=\""avc1.4d401f\"" width=\""396\"" height=\""704\"" frameRate=\""16000/528\"" sar=\""1:1\"" startWithSAP=\""1\"" bandwidth=\""766114\"" FBQualityClass=\""sd\"" FBQualityLabel=\""396w\""\u003e\u003cBaseURL\u003ehttps://scontent-cdg2-1.cdninstagram.com/t72.12950-16/10000000_130426674268267_3521803917782417408_n.mp4\u003c/BaseURL\u003e\u003cSegmentBase indexRangeExact=\""true\"" indexRange=\""899-4014\""\u003e\u003cInitialization range=\""0-898\""/\u003e\u003c/SegmentBase\u003e\u003c/Representation\u003e\u003c/AdaptationSet\u003e\u003cAdaptationSet segmentAlignment=\""true\"" lang=\""und\"" subsegmentAlignment=\""true\"" subsegmentStartsWithSAP=\""1\""\u003e\u003cRepresentation id=\""17848696471221278a\"" mimeType=\""audio/mp4\"" codecs=\""mp4a.40.2\"" audioSamplingRate=\""44100\"" startWithSAP=\""1\"" bandwidth=\""50493\""\u003e\u003cAudioChannelConfiguration schemeIdUri=\""urn:mpeg:dash:23003:3:audio_channel_configuration:2011\"" value=\""2\""/\u003e\u003cBaseURL\u003ehttps://scontent-cdg2-1.cdninstagram.com/t72.12950-16/19104379_545857249086594_8255680261132386304_n.mp4\u003c/BaseURL\u003e\u003cSegmentBase indexRangeExact=\""true\"" indexRange=\""835-3878\""\u003e\u003cInitialization range=\""0-834\""/\u003e\u003c/SegmentBase\u003e\u003c/Representation\u003e\u003c/AdaptationSet\u003e\u003c/Period\u003e\u003c/MPD\u003e"",
				""expire_at"": 1510987021,
				""encoding_tag"": ""instagram_dash_remuxed"",
				""internal_only"": false,
				""number_of_qualities"": 1,
				""cover_frame_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.9792-15/16994044_137486973681364_2459237027455959040_n.jpg"",
				""broadcast_owner"": {
					""pk"": 1383543459,
					""username"": ""780ir"",
					""full_name"": ""\u0647\u0641\u0640 \u0647\u0634\u062a\u0627\u062f | #\u0667\u0668\u0660*"",
					""is_private"": false,
					""profile_pic_url"": ""https://scontent-cdg2-1.cdninstagram.com/t51.2885-19/s150x150/21980259_360063074444115_3710275616830914560_n.jpg"",
					""profile_pic_id"": ""1532382334209046138_1383543459"",
					""friendship_status"": {
						""following"": true,
						""followed_by"": false,
						""blocking"": false,
						""is_private"": false,
						""incoming_request"": false,
						""outgoing_request"": false,
						""is_bestie"": false
					},
					""is_verified"": false
				},
				""published_time"": 1510900081,
				""media_id"": ""1649871864962943492_1383543459"",
				""broadcast_message"": """",
				""organic_tracking_token"": ""eyJ2ZXJzaW9uIjo1LCJwYXlsb2FkIjp7ImlzX2FuYWx5dGljc190cmFja2VkIjp0cnVlLCJ1dWlkIjoiNWM5NGNlZjkxYWViNDYwMzkzNWFhZDMxOTQ2NjMyNjMxNjQ5ODcxODY0OTYyOTQzNDkyIiwic2VydmVyX3Rva2VuIjoiMTUxMDkxNzMxMjM5M3wxNjQ5ODcxODY0OTYyOTQzNDkyfDE2NDc3MTg0MzJ8NTdlMThmMjZjZGZmZTE4ZTI4OGU1ODU3NDAzNzFkMzhmZmU1ZWMxYmI0NzYzOTcyMTYyNzM5YTBhMjczYTQ5YSJ9LCJzaWduYXR1cmUiOiIifQ==""
			}],
			""last_seen_broadcast_ts"": 0,
			""ranked_position"": 15,
			""seen_ranked_position"": 0,
			""muted"": false,
			""can_reply"": false,
			""can_reshare"": false
		}]
	},
	""story_ranking_token"": ""6ed23f2e-b27b-4dc3-beba-a19a0439e384"",
	""broadcasts"": [],
	""face_filter_nux_version"": 4,
	""has_new_nux_story"": false,
	""status"": ""ok""
}";

        [Fact]
        public void ConvertReelFeedTest()
        {
            var storyFeedResponse = JsonConvert.DeserializeObject<InstaStoryFeedResponse>(testJson);
            var fabric = ConvertersHelper.GetDefaultFabric();
            var result = fabric.GetStoryFeedConverter(storyFeedResponse).Convert();
            Assert.NotNull(result);
        }
    }
}