
create trigger ValidateCharacterData on Characters for insert 
as
declare @maxTotal int;
set @maxTotal = 8;
declare @insertedTotal int;
set @insertedTotal = (select Attack from inserted) + (select MagicDefense from inserted) 
+ (select Health from inserted) + (select PhysicalDefense from inserted)

if @insertedTotal>@maxTotal
begin
rollback transaction;
return;
end;
