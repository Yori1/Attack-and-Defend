create trigger ValidateCharacterData on Characters for insert, update
as
begin
	declare @maxTotal int;
	set @maxTotal = 8;
	declare @insertedTotal int;
	set @insertedTotal = (select BaseAttack from inserted) + (select BaseMagicDefense from inserted) 
	+ (select BaseMaximumHealth from inserted) + (select BasePhysicalDefense from inserted)

	if @insertedTotal>@maxTotal
	begin
		rollback transaction;
		return;
	end;
end;

