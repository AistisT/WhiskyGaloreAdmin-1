CREATE PROCEDURE `new_procedure` (in idate date)
BEGIN
select * from dailyhours where currentDate=idate;  
END