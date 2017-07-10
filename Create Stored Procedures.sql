DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Add_Tag`(In TagIn Varchar(254))
BEGIN
	Insert into tags values (TagIn);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Autocomplete_Actor`(IN SearchTerm Varchar(254))
BEGIN
	select Name from actors where Name like concat(SearchTerm,'%');
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Autocomplete_City`(IN SearchTerm Varchar(254))
BEGIN
	select distinct City from locations where City like concat(SearchTerm,'%');
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Autocomplete_Country`(IN SearchTerm Varchar(254))
BEGIN
	select Name from countries where Name like concat(SearchTerm,'%');
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Autocomplete_Director`(IN SearchTerm Varchar(254))
BEGIN
	select Name from directors where Name like concat(SearchTerm,'%');
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Autocomplete_Genre`(IN SearchTerm Varchar(254))
BEGIN
	select Genres from genres where Genres like concat(SearchTerm,'%');
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Autocomplete_Region`(IN SearchTerm Varchar(254))
BEGIN
	select distinct Region from locations where Region like concat(SearchTerm,'%');
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Autocomplete_Tag`(IN SearchTerm Varchar(254))
BEGIN
	select Name from tags where Name like concat(SearchTerm,'%');
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Favorite_Film_Delete`(IN UserIDIn int, IN MovieIDIn Int)
BEGIN
	Delete from favoritefilms where userID=userIDIn and MovieID=MovieIDIn;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Favorite_Films`(IN UserIDIn int)
BEGIN
	Select m.Title,m.rating,m.Photo,m.Year from favoriteMovies fm
    inner join movies m on fm.MovieID=m.movieid
    where fm.userID=UserIDIn;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Favorite_Films_Insert`(IN UserIDIn int, IN MovieIDIn int)
BEGIN
	Insert into favoritefilms values (userIDIn, MovieIDIn);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Favorite_Genre`(IN UserIDIn int)
BEGIN
	Select GenreName from favoriteGenres where userID=UserIDIn;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Favorite_Genre_Delete`(IN UserIDIn int, IN GenreIn Varchar(254))
BEGIN
	Delete from favoriteGenres where userID=userIDIn and GenreName=GenreIn;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Favorite_Genre_Insert`(IN UserIDIn int, IN GenreIn Varchar(254))
BEGIN
	Insert into favoriteGenres values (userIDIn, GenreIn);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Favorite_Tag`(IN UserIDIn int)
BEGIN
	Select TagName from favoriteTags where userID=UserIDIn;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Favorite_Tag_Delete`(IN UserIDIn int, IN TagIn Varchar(254))
BEGIN
	Delete from favoriteTags where userID=userIDIn and TagName=TagIn;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Favorite_Tag_Insert`(IN UserIDIn int, IN TagIn Varchar(254))
BEGIN
	Insert into favoriteTags values (userIDIn, TagIn);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Movie_Info`(IN MovieID INT)
BEGIN
	SELECT distinct m.title as 'Title',m.rating as 'Rating',d.Name as 'Director',
    (
		select group_concat( distinct mg.GenreName separator ', ' ) 
		from dbo.movieofgenre mg 
		where mg.movieid=MovieID) as 'Genres',
    (
		select group_concat( distinct a.name separator ', ' ) 
		from dbo.starring s 
		inner join dbo.actors a on a.actorid=s.actorid
		where s.movieid=MovieID
    ) as 'Actors',
    (
		select group_concat( distinct concat(fi.city,', ',fi.region) separator ', ' ) 
		from dbo.filmedin fi 
		where fi.movieid=MovieID
    ) as 'Film Locations',
    m.Countryofproduction as 'Country of Origin',
    (
		select group_concat( distinct mt.TagName separator ', ' ) 
		from dbo.movietaggedas mt 
		where mt.movieid=MovieID
    ) as 'Tags',
    m.Photo as 'Photo'
    
    from dbo.movies m
    Left outer join dbo.directors d on m.DirectorID=d.DirectorID
    where m.movieid=MovieID;
    
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Search_Results`(IN TitleIn VARCHAR(254), IN RatingIn Decimal(5,4),
 IN DirectorIn VARCHAR(254), IN GenreIn VARCHAR(254), IN ActorIn Varchar(254), IN FilmLocationIn Varchar(254),
 IN CountryIn VARCHAR(254), IN TagIn Varchar(254))
BEGIN
	SELECT distinct m.MovieID from dbo.movies m
    Left outer join dbo.directors d on m.DirectorID=d.DirectorID
    left outer join dbo.movieofgenre mg on mg.movieid=m.movieid
    left outer join dbo.starring s on s.MovieID=m.movieID
    left outer join dbo.actors a on s.actorid=a.actorid
    left outer join dbo.filmedIn fi on fi.movieID=m.movieID
    left outer join dbo.movietaggedas mt on mt.movieid=m.movieid
    WHERE (titleIn is null or m.title like Concat('%',titleIn,'%'))
    AND (ratingIn is null or m.rating >= RatingIn)
    AND (DirectorIn is null or d.name like Concat('%',DirectorIn,'%'))
    AND (GenreIn is null or mg.GenreName like concat('%',GenreIn,'%'))
    AND (ActorIn is null or a.name like concat('%',ActorIn,'%'))
    AND (ActorIn is null or a.name like concat('%',ActorIn,'%'))
    AND (FilmLocationIn is null or fi.region like concat('%',FilmLocationIn,'%') or fi.city like concat('%',FilmLocationIn,'%'))
    AND (CountryIn is null or m.countryofproduction like concat('%',CountryIn,'%'))
    AND (TagIn is null or mt.TagName like concat('%',TagIn,'%'))
    
    
    ;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Tag_Movie`(IN MovieIDIN int, In TagIn Varchar(254))
BEGIN
	Insert into movietaggedas values (MovieIDIN,TagIn);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Tag_Movie_Remove`(IN MovieIDIN int, In TagIn Varchar(254))
BEGIN
	Delete from movietaggedas where movieID=MovieIDIN and TagName=TagIn;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `User_Info`(IN UserIDIn int)
BEGIN
	Select * from users where ID=userIDin;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `User_Info_Create_All`(IN UserIDIn int,IN NameIn Varchar(254)
, IN PasswordIn VarChar(254), IN RatingIn Decimal(8,0))
BEGIN
	Insert into users values (UserIDIn, NameIn,PasswordIn,RatingIn);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `User_Info_Delete_All`(IN UserIDIn int)
BEGIN
	Delete from users where ID=userIDin;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `User_Info_Next_ID`()
BEGIN
	Select Max(ID)+1 from users;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `User_Info_Update_Name`(IN UserIDIn int,IN NameIn varchar(254))
BEGIN
	Update users set name=NameIn where ID=userIDin;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `User_Info_Update_Password`(IN UserIDIn int,IN PasswordIn varchar(254))
BEGIN
	Update users set passwordhash=PasswordIn where ID=userIDin;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `User_Info_Update_Rating`(IN UserIDIn int,IN RatingIn Decimal(8,0))
BEGIN
	Update users set minrating=ratingIn where ID=userIDin;
END$$
DELIMITER ;
