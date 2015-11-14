-- Aistis
CREATE PROCEDURE `insertNewCategory` (in icategoryName varchar(45), in idescription varchar(45), in icategoryInUse tinyint(1),in iimageData longblob, iimageName varchar(255), in ifileSize int(10),  in iimageType varchar(45))
BEGIN
DECLARE picid int(10);

DECLARE exit handler for sqlexception
  BEGIN
    -- ERROR
  ROLLBACK;
END;
DECLARE exit handler for sqlwarning
 BEGIN
    -- WARNING
 ROLLBACK;
END;



START TRANSACTION;
insert into pictures(imageData,imageName,fileSize,imageType)
values (iimageData,iimageName,ifileSize,iimageType);


set picid = LAST_INSERT_ID();
insert into category(categoryName,description,categoryInUse,Pictures_picturesId)
values (icategoryName,idescription,icategoryInUse,picid);

COMMIT;
END