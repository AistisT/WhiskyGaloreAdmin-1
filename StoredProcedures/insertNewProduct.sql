-- Aistis
CREATE DEFINER=`bfe098b52836a5`@`%` PROCEDURE `insertNewProduct`(in ivendorProductId varchar(100),in iproductName varchar(100), in idescription varchar(1000), in iunitPrice float, in imsrp float, in iunitWeight float, in iquantityPerUnit int(10), in iimageData longblob, iimageName varchar(255), in ifileSize int(10),  in iimageType varchar(45),in iCategory_categoryId int(10))
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

insert into product(vendorProductId,productName,description,unitPrice,msrp,unitWeight,quantityPerUnit,Category_categoryId,Pictures_picturesId)
values (ivendorProductId,iproductName,idescription,iunitPrice,imsrp,iunitWeight,iquantityPerUnit,iCategory_categoryId,picid);

COMMIT;
END