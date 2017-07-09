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
