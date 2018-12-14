CREATE TRIGGER LogUserActions on AspNetUsers
after insert 
as
begin
insert into UserLog
(LogDateTime, LogDescription, UserId, Username)
select getdate(), 'User has been inserted.', i.Id, i.UserName
from AspNetUsers u
inner join inserted i on u.Id=i.Id
end