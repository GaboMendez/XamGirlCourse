# Homework 05

* Use an API in your selection, request an API and display the information in the view.
 
* The app must validate that there is an internet connection before making the request, if there is no internet connection it should show an error message.
 
## API Used
-[Jikan API](https://jikan.moe/)
* Jikan is an open-source PHP & REST API for the “most active online anime + manga community and database” — MyAnimeList. It parses the website to satisfy the need for an API.

-**EndPoints Used**
* https://api.jikan.moe/v3/top/anime/{page_number}: This endpoint is used for getting the top items on MyAnimeList of an indicated page.

* https://api.jikan.moe/v3/search/anime?q={anime_name}: This endpoint is used for getting the result for the searched anime. *NOTE: MyAnimeList only processes queries with a minimum of 3 letters.*

* https://api.jikan.moe//v3/anime/{anime_id}: This endpoint is used for getting a single anime object with all its details with the anime ID.

-[Documentation API](https://jikan.docs.apiary.io/#)

## Screenshots
<p aling="center">
<img src="/ScreenShots/Screen01.jpg" width="30%" /> <img src="/ScreenShots/Screen02.jpg" width="30%" /> 
</p>
<p aling="center">
<img src="/ScreenShots/Screen03.jpg" width="30%" /> <img src="/ScreenShots/Screen04.jpg" width="30%" />  
</p>
 
 #### Made by: *Gabriel Mendez Reyes*
