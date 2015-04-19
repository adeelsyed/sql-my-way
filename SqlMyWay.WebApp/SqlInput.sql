select * from student s join enrollment e on e.studentid = s.studentid join course c on c.courseid = e.courseid;
/* I added this block comment and 
   the line above for extra testing */
SELECT  
	term, 
	student_id,
    CASE
		WHEN ((ft_credits > 0 AND credits >= ft_credits) OR (ft_hours_per_week > 3 AND hours_per_week >= ft_hours_per_week)) THEN 'F'
        ELSE 'P'
        END AS status
FROM    
	(
        SELECT  
			term, 
			student_id,
            pm.credits AS ft_credits, 
			pm.hours AS ft_hours_per_week,
            SUM(credits) AS credits, 
			SUM(hours_per_week) AS hours_per_week
        FROM    
			(
				SELECT  
					e.term, 
					e.student_id, 
					ISNULL(o.credits, 0) credits,
					CASE
						WHEN ISNULL(o.weeks, 0) > 5 THEN (ISNULL(o.lect_hours, 0) + ISNULL(o.lab_hours, 0)) / ISNULL(o.weeks, 0)
						ELSE 0
						END AS hours_per_week
				FROM    
					enrollment AS e
					INNER JOIN program_enrollment AS pe ON e.student_id = pe.student_id
				WHERE   
					e.registration_code NOT IN ('A7', 'D0', 'WL')
			) as A
			INNER JOIN program_major AS pm ON sh.major_code_1 = pm._major_code
        WHERE
			sh.eff_term = 
				(
					SELECT 
						MAX(eff_term)
                    FROM 
						student_history AS shi
                    WHERE 
						sh.student_id = shi.student_id
						AND shi.eff_term <= term
				)
        GROUP BY 
			term, 
			student_id, 
			pm.credits, 
			pm.hours
    ) B
ORDER BY 
	term, 
	student_id