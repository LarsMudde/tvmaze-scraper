# TVMaze-scraper
This Scraper scrapes the data from the [TVMaze API](https://www.tvmaze.com/api) using a hosted service and stores the data in a relational database using Entity Framework Core. The data is then exposed trough a rest api that allows the user to query the TVShows by name and returns a paginated result. This rest api also has SwaggerUI, by default you can find SwaggerUI here http://localhost:58370/swagger/index.html.

### Instructions
- Please make sure to update the database before running the application by running the "update-database" command in the NuGet package manager console.
- The application uses Polly to help deal with the rate limit of the TVMaze API. However if you set the interval (in appsettings.json) too low, you will surely reach the maximum amount of retries soon and miss out on data.
- `API calls are rate limited to allow at least 20 calls every 10 seconds per IP address.`

### Considerations:
- Currently the scraper just counts upwards (from 1 to the limit of int). It would be nice if there was a range specified and the Scraper could just keep scraping that range.
- I have gone a little bit overboard with the Dto's and mappers, this could still be reduced but I liked the idea that I could store more data from the api without having to change the architecture of the application, I just have to expand the dto's and models. 
- In the future it would be better/wise to have the Id's from the Actor and TVShow not be the database Id. Because of the high speed of the scraper, and the scraper being asynchronous it can sometimes cause attempts to insert duplicate keys for the actor which will at this moment cause an exception, this could also be solved by changing the scope of the DbContext.
- I have opted for 2 separate mappers with separate configurations, it might be nicer to combine these in the future.
- It would be nice to add unit tests and more ways to query the database, and save more data from the scraper to the database.
- Decouple the Scraper and the REST API and put them in different services.
- It would be nice to have better exception handling.
- Add Unit Tests
