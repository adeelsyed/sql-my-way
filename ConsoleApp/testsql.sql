--simplest select
select col1, [select+from] from tab1 where col1 = 5

--all select clauses
select col1, col2 into tab1 from tab2 where col1 = 5 and col2 = 1 group by col1, col2 having count(*) > 1 order by col3
