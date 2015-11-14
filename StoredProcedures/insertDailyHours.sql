CREATE DEFINER=`bfe098b52836a5`@`%` PROCEDURE `insertNewDailyHours`(in  icurrentDate date, in  ihoursWorked float, in  inStaff_staffId int(10))
BEGIN

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
insert ignore into dailyFinances(date)
values (icurrentDate);

insert into dailyHours(currentDate,hoursWorked,Staff_staffId,dailyfinances_date)
values (icurrentDate,ihoursWorked,inStaff_staffId,icurrentDate);

COMMIT;
END