# AdOptimizer

Contains 2 endpoints: 
* **[POST] OptimizeAd** - Checks if text with the provided title, description, and keywords fits in the character limit for the specified platform. And returns the optimized text to fit the maximum amount of provided data.
* **[GET] GetSupportedPlatforms** - Returns all social media platforms that have a character limit lower than the provided one. If no limit is provided, return all known social platforms.

## Instructions
1. Clone this repository
2. Start the project
3. Test it

* **Via Swagger:**
  1. You will be redirected to Swagger in your browser or go directly to https://localhost:7095/index.html
  2. Choose the endpoint
  3. Press "Try it out" button
  4. Enter data
  5. Press "Execute" button

* **Via Postman:**
  1. Go to Postman or you can find link with examples below:
  2. Use link with examples or one of the endpoint links:
     * https://localhost:7095/api/get-supported-platforms
     * https://localhost:7095/api/optimize-ad
    _Ready examples:_ https://www.postman.com/satellite-pilot-10362965/adoptimizer/collection/38241899-1f03154e-3e8f-44cf-9b24-b01a4fe57726/?action=share&creator=38241899
  3. Enter parameters
  4. Press "Send" button
 
## Examples
If you want to export examples to Postman via raw data, you can find it below:
<details>
  <summary>Click to reveal the json text</summary>

```json

  {
	"info": {
		"_postman_id": "1f03154e-3e8f-44cf-9b24-b01a4fe57726",
		"name": "AdOptimizer",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
		"_exporter_id": "38241899"
	},
	"item": [
		{
			"name": "GetSupportedPlatforms",
			"item": [
				{
					"name": "SmallLimit",
					"request": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/get-supported-platforms?CharacterLimit=200",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"get-supported-platforms"
							],
							"query": [
								{
									"key": "CharacterLimit",
									"value": "200"
								}
							]
						}
					},
					"response": []
				},
				{
					"name": "MiddleLimit",
					"request": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/get-supported-platforms?CharacterLimit=300",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"get-supported-platforms"
							],
							"query": [
								{
									"key": "CharacterLimit",
									"value": "300"
								}
							]
						}
					},
					"response": []
				},
				{
					"name": "BigLimit",
					"request": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/get-supported-platforms?CharacterLimit=2000",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"get-supported-platforms"
							],
							"query": [
								{
									"key": "CharacterLimit",
									"value": "2000"
								}
							]
						}
					},
					"response": []
				},
				{
					"name": "NoLimit",
					"request": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/get-supported-platforms",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"get-supported-platforms"
							]
						}
					},
					"response": []
				}
			]
		},
		{
			"name": "OptimizeAd",
			"item": [
				{
					"name": "EmptyRequest",
					"request": {
						"method": "POST",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/optimize-ad",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"optimize-ad"
							]
						}
					},
					"response": []
				},
				{
					"name": "EmptyPlatform",
					"request": {
						"method": "POST",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/optimize-ad?Title=title&Platform= ",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"optimize-ad"
							],
							"query": [
								{
									"key": "Title",
									"value": "title"
								},
								{
									"key": "Platform",
									"value": " "
								}
							]
						}
					},
					"response": []
				},
				{
					"name": "UnknownPlatform",
					"request": {
						"method": "POST",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/optimize-ad?Title=title&Platform=test",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"optimize-ad"
							],
							"query": [
								{
									"key": "Title",
									"value": "title"
								},
								{
									"key": "Platform",
									"value": "test"
								}
							]
						}
					},
					"response": []
				},
				{
					"name": "TextWithIntroTitleDescriptionKeywords",
					"request": {
						"method": "POST",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/optimize-ad?Title=.Net developer&Description=This is the best job&Keywords=software&Keywords=developer&Platform=twitter",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"optimize-ad"
							],
							"query": [
								{
									"key": "Title",
									"value": ".Net developer"
								},
								{
									"key": "Description",
									"value": "This is the best job"
								},
								{
									"key": "Keywords",
									"value": "software"
								},
								{
									"key": "Keywords",
									"value": "developer"
								},
								{
									"key": "Platform",
									"value": "twitter"
								}
							]
						}
					},
					"response": []
				},
				{
					"name": "TextWithIntroTitleLink",
					"request": {
						"method": "POST",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/optimize-ad?Title=.Net developer&Description=Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere, mi et blandit convallis, justo purus consectetur dui, id tempor neque est sit amet nunc. Suspendisse lacus urna, maximus sodales tristique sed, bibendum at nibh. Maecenas viverra scelerisque est id vehicula. Nam maximus metus.&Platform=twitter",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"optimize-ad"
							],
							"query": [
								{
									"key": "Title",
									"value": ".Net developer"
								},
								{
									"key": "Description",
									"value": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere, mi et blandit convallis, justo purus consectetur dui, id tempor neque est sit amet nunc. Suspendisse lacus urna, maximus sodales tristique sed, bibendum at nibh. Maecenas viverra scelerisque est id vehicula. Nam maximus metus."
								},
								{
									"key": "Platform",
									"value": "twitter"
								}
							]
						}
					},
					"response": []
				},
				{
					"name": "TextWithIntroTitleLinkKeywords",
					"request": {
						"method": "POST",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/optimize-ad?Title=.Net developer&Description=Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere, mi et blandit convallis, justo purus consectetur dui, id tempor neque est sit amet nunc. Suspendisse lacus urna, maximus sodales tristique sed, bibendum at nibh. Maecenas viverra scelerisque est id vehicula. Nam maximus metus.&Platform=twitter",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"optimize-ad"
							],
							"query": [
								{
									"key": "Title",
									"value": ".Net developer"
								},
								{
									"key": "Description",
									"value": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere, mi et blandit convallis, justo purus consectetur dui, id tempor neque est sit amet nunc. Suspendisse lacus urna, maximus sodales tristique sed, bibendum at nibh. Maecenas viverra scelerisque est id vehicula. Nam maximus metus."
								},
								{
									"key": "Platform",
									"value": "twitter"
								}
							]
						}
					},
					"response": []
				},
				{
					"name": "TextWithIntroTitleLink",
					"request": {
						"method": "POST",
						"header": [],
						"url": {
							"raw": "https://localhost:7095/api/optimize-ad?Title=.Net developer&Platform=twitter",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "7095",
							"path": [
								"api",
								"optimize-ad"
							],
							"query": [
								{
									"key": "Title",
									"value": ".Net developer"
								},
								{
									"key": "Platform",
									"value": "twitter"
								}
							]
						}
					},
					"response": []
				}
			]
		}
	]
}
  
