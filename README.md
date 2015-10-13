RatingWidget C# SDK
====================

Non-Official RatingWidget C# SDK created by [Pablo Gimenez](https://github.com/PabloGim)

## API Authentication

To get your site's ID, Public Key & Secret Key, simply sign-in at http://app.rating-widget.com and open the relevant site details. Complete the Helper class with the info:

    public const string publicKey = "HERE GOES YOUR PUBLIC KEY";
    public const string secretKey = "HERE GOES YOUR SECRET KEY";
    public const string RWId = "HERE GOES YOUR ID";

## Usage Example

Get the total count of ratings

	var numRatings = Helper.CallRWAPI("/v1/sites/" + Helper.RWId + "/ratings/count.json", "GET");
	
Get Snippet info and deserialize to object
	
    var snippetsJSON = Helper.CallRWAPI("/v1/sites/" + Helper.RWId + "/ratings/rich-snippets.json", "GET");
    RatingSnippets snippets = JsonConvert.DeserializeObject<RatingSnippets>(snippetsJSON);

Loading the first 100 site's ratings:

    for (int i = 0; i < 2; i++)
    {
        var response = Helper.CallRWAPI("/v1/sites/" + Helper.RWId + "/ratings.json", "GET", "?count=50&offset=" + i * 50);
	}
	
Delete a specified rating:

    var responseDelete = Helper.CallRWAPI("/v1/sites/" + Helper.RWId + "/ratings/" + rating.id + ".json", "DELETE");
    
Note: Please do NOT use ratings.json call for Rich-Snippets, there's a special call for that.
    
Clear specified rating's votes:

	var responseDelete = Helper.CallRWAPI("/v1/sites/" + Helper.RWId + "/ratings/" + rating.id + "/votes.json", "DELETE");
