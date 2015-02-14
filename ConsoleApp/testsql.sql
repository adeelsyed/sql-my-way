select Column1, Column2, Column1 + Column2 as SumOfCols from MyTable m 
--00
where m.Id > 0


select * from phone /*ggg*/ where phonenumber = '9256910800'
				--set transaction isolation level read uncommitted
				--update Phone
				--set PhoneNumber = '3107175106'
				--where PhoneNumber = '9256910800'

select * from phone where phoneid in (34861465,50327981)

exec prcIVRGetAccountBalance '3107175106'