CREATE DEFINER=`bfe098b52836a5`@`%` PROCEDURE `getStaffIdName`()
BEGIN
select staff.staffId , contact.forename,  contact.surname
	from staff
	inner join contact
    on staff.contact_contactID=contact.contactID
order by  contact.suername;
END