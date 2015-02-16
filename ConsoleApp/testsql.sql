--ad hoc test


--simplest select
select col1 from tab1 where col1 = 5;select col1 from tab1 where col1 = 5
go

--all select clauses, lists
with tab2 as (select * from site) select t2.col1, col2, col1+col2 as col3 into tab1 from tab2 t2 join tab3 t3 on t3.ID = t2.ID where col1 = 5 and (col2 = 1 or col2 = 3) group by col1, col2, col1+col2 having count(*) > 1 order by col1, /*mlc*/ col2, col3