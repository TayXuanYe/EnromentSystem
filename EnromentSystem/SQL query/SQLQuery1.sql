USE EnrolmentSystemDatabase
INSERT INTO student 
(sid,name,password,program,student_email,ic_or_passport,mode_of_study,school,level,major,scholarship,admission_date) VALUES 
('I23024312','Tay Xuan Ye','iu040804130465','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)','i23024312@student.newinti.edu.my','040804130465','Full-time Learning','GBL110 INFORMATION TECHNOLOGY','Degree','SOFTWARE ENGINEERING',60,'2024-01-01')

INSERT INTO lecture
(lid,name,password) values
('L00000001','test lecturer 1','test1'),
('L00000002','test lecturer 2','test2')
/*school:'GBL000 TEST'*/
INSERT INTO school values('GBL000 TEST')
INSERT INTO program VALUES('TTTT - TEST','GBL000 TEST','Foundation')
/*school:'GBL110 INFORMATION TECHNOLOGY'*/
INSERT INTO school values('GBL110 INFORMATION TECHNOLOGY')
/*Foundation*/
INSERT INTO program VALUES('FDIT - FOUNDATION IN INFORMATION TECHNOLOGY','GBL110 INFORMATION TECHNOLOGY','Foundation')
/*Certificate*/
INSERT INTO program VALUES('CIIT - CERTIFICATE IN INFORMATION TECHNOLOGY','GBL110 INFORMATION TECHNOLOGY','Certificate')
/*Diploma*/
INSERT INTO program VALUES('DIIT - DIPLOMA IN INFORMATION TECHNOLOGY','GBL110 INFORMATION TECHNOLOGY','Diploma')
INSERT INTO program VALUES('DICS - DIPLOMA IN COMPUTER SCIENCE','GBL110 INFORMATION TECHNOLOGY','Diploma')
/*Degree*/
/*BCSI*/
INSERT INTO program VALUES('BCSI - BACHELER OF COMPUTER SCIENCES (HONS)','GBL110 INFORMATION TECHNOLOGY','Degree')
INSERT INTO major VALUES('SOFTWARE ENGINEERING','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)')
INSERT INTO major VALUES('NETWORK AND SECURITY','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)')
INSERT INTO major VALUES('MOBILE COMPUTING','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)')
INSERT INTO major VALUES('CLOUD COMPUTING','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)')
INSERT INTO major VALUES('BUSINESS ANALYTICS','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)')
/*BITI*/
INSERT INTO program VALUES('BITI - BACHELOR OF INFORMATION TECHNOLOGY(HONS)','GBL110 INFORMATION TECHNOLOGY','Degree')
INSERT INTO major VALUES('BUSINESS ANALYTICS','BITI - BACHELOR OF INFORMATION TECHNOLOGY(HONS)')
INSERT INTO major VALUES('DATA SCIENCE','BITI - BACHELOR OF INFORMATION TECHNOLOGY(HONS)')
/*Master*/
INSERT INTO program VALUES('MIS - MASTER IN INFORMATION SYSTEMS','GBL110 INFORMATION TECHNOLOGY','Master')
INSERT INTO program VALUES('MIT - MASTER IN INFORMATION TECHNOLOGY','GBL110 INFORMATION TECHNOLOGY','Master')
/*PhD*/
INSERT INTO program VALUES('PHDDS - DOCTOR OF PHILOSOPHY IN DATA SCIENCE','GBL110 INFORMATION TECHNOLOGY','PhD')

/*PRG3201*/
INSERT INTO course 
(cid,name,credit_hours,available,price) VALUES
('PRG3201','OBJECT ORIENTED PROGRAMMING',4,1,2555)
INSERT INTO course_major
(cid,major,program) VALUES
('PRG3201','NONE','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)')
INSERT INTO course_major
(cid,major,program) VALUES
('PRG3201','NONE','BITI - BACHELOR OF INFORMATION TECHNOLOGY(HONS)')


INSERT INTO section
(sid, name, cid, semester, program, max_enroll, current_enroll) VALUES
('PRG3201-AUG2024-8G1', '8G1', 'PRG3201', 'AUG2024', 'BCSI - BACHELER OF COMPUTER SCIENCES (HONS)', 30, 0);

INSERT INTO section
(sid, name, cid, semester, program, max_enroll, current_enroll) VALUES
('PRG3201-AUG2024-8G2', '8G2', 'PRG3201', 'AUG2024', 'BCSI - BACHELER OF COMPUTER SCIENCES (HONS)', 30, 0);

INSERT INTO section
(sid, name, cid, semester, program, max_enroll, current_enroll) VALUES
('PRG3201-AUG2024-8H1', '8H1', 'PRG3201', 'AUG2024', 'BITI - BACHELOR OF INFORMATION TECHNOLOGY(HONS)', 30, 0);

INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8G1-1', 'PRG3201-AUG2024-8G1', 40, 'C1', 'L00000001', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8G1-2', 'PRG3201-AUG2024-8G1', 41, 'C1', 'L00000001', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8G1-3', 'PRG3201-AUG2024-8G1', 8, 'C1', 'L00000001', 'PRACTICAL');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8G1-4', 'PRG3201-AUG2024-8G1', 9, 'C1', 'L00000001', 'PRACTICAL');

INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8G2-1', 'PRG3201-AUG2024-8G2', 42, 'C1', 'L00000001', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8G2-2', 'PRG3201-AUG2024-8G2', 43, 'C1', 'L00000001', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8G2-3', 'PRG3201-AUG2024-8G2', 7, 'C1', 'L00000001', 'PRACTICAL');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8G2-4', 'PRG3201-AUG2024-8G2', 6, 'C1', 'L00000001', 'PRACTICAL');

INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8H1-1', 'PRG3201-AUG2024-8H1', 42, 'C1', 'L00000001', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8H1-2', 'PRG3201-AUG2024-8H1', 43, 'C1', 'L00000001', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8H1-3', 'PRG3201-AUG2024-8H1', 7, 'C1', 'L00000001', 'PRACTICAL');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3201-AUG2024-8H1-4', 'PRG3201-AUG2024-8H1', 6, 'C1', 'L00000001', 'PRACTICAL');


/*PRG3204*/
INSERT INTO course
(cid,name,credit_hours,available,price) VALUES 
('PRG3204','WEB APPLICATION DEVELOPMENT',4,1,2555)
INSERT INTO course_major
(cid,major,program) VALUES
('PRG3204','SOFTWARE ENGINEERING','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)')
INSERT INTO course_prerequisite 
(cid,prerequisite) VALUES
('PRG3204','PRG3201')
INSERT INTO section
(sid, name, cid, semester, program, max_enroll, current_enroll) VALUES
('PRG3204-AUG2024-8G1', '8G1', 'PRG3204', 'AUG2024', 'BCSI - BACHELER OF COMPUTER SCIENCES (HONS)', 30, 0);

INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3204-AUG2024-8G1-1', 'PRG3204-AUG2024-8G1', 42, 'C1', 'L00000002', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3204-AUG2024-8G1-2', 'PRG3204-AUG2024-8G1', 43, 'C1', 'L00000002', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3204-AUG2024-8G1-3', 'PRG3204-AUG2024-8G1', 7, 'C1', 'L00000002', 'PRACTICAL');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3204-AUG2024-8G1-4', 'PRG3204-AUG2024-8G1', 6, 'C1', 'L00000002', 'PRACTICAL');

/*PRG3205*/
INSERT INTO course
(cid,name,credit_hours,available,price) VALUES 
('PRG3205','ALGORITHM',4,1,2555)
INSERT INTO course_major
(cid,major,program) VALUES
('PRG3205','SOFTWARE ENGINEERING','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)')
INSERT INTO section
(sid, name, cid, semester, program, max_enroll, current_enroll) VALUES
('PRG3205-JAN2024-8G1', '8G1', 'PRG3205', 'JAN2024', 'BCSI - BACHELER OF COMPUTER SCIENCES (HONS)', 30, 0);
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3205-JAN2024-8G1-1', 'PRG3205-JAN2024-8G1', 44, 'C1', 'L00000001', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3205-JAN2024-8G1-2', 'PRG3205-JAN2024-8G1', 45, 'C1', 'L00000001', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3205-JAN2024-8G1-3', 'PRG3205-JAN2024-8G1', 1, 'C1', 'L00000001', 'PRACTICAL');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3205-JAN2024-8G1-4', 'PRG3205-JAN2024-8G1', 2, 'C1', 'L00000001', 'PRACTICAL');

INSERT INTO section
(sid, name, cid, semester, program, max_enroll, current_enroll) VALUES
('PRG3205-AUG2024-8G1', '8G1', 'PRG3205', 'AUG2024', 'BCSI - BACHELER OF COMPUTER SCIENCES (HONS)', 30, 0);
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3205-AUG2024-8G1-1', 'PRG3205-AUG2024-8G1', 44, 'C1', 'L00000001', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3205-AUG2024-8G1-2', 'PRG3205-AUG2024-8G1', 45, 'C1', 'L00000001', 'LECTURE');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3205-AUG2024-8G1-3', 'PRG3205-AUG2024-8G1', 1, 'C1', 'L00000001', 'PRACTICAL');
INSERT INTO class
(id, sid, time, class_room, lid, type) VALUES
('PRG3205-AUG2024-8G1-4', 'PRG3205-AUG2024-8G1', 2, 'C1', 'L00000001', 'PRACTICAL');

INSERT INTO previous_compulsory_course
(sid,cid) VALUES 
('I23024312','PRG3201')

INSERT INTO student_taken_course
(sid,cid,section_id,status) VALUES
('I23024312','PRG3201','PRG3201-AUG2024-8G2','FAIL')


SELECT * FROM course WHERE cid NOT IN (SELECT cid FROM request_add_course )

INSERT INTO hop
(hid,fullname,password,email) VALUES
('H18016442','CaiYi','iuee4769449','i23024312@student.newinti.edu.my')



INSERT INTO section
(sid, name, cid, semester, program, max_enroll, current_enroll) VALUES
('PRG3204-AUG2024-8G2', '8G2', 'PRG3204', 'AUG2024', 'BCSI - BACHELER OF COMPUTER SCIENCES (HONS)', 30, 0);

INSERT INTO student_taken_course
(sid,cid,section_id,status) VALUES
('I23024312','PRG3204','PRG3204-AUG2024-8G1','TAKEN')

//delete
INSERT INTO request_add_course
(sid,cid,section_id,reason,status) VALUES
('I23024312','PRG3205','PRG3205-JAN2024-8G1','no','PENDING')
INSERT INTO request_change_section
(sid,cid,current_section_id,target_section_id,reason,status) VALUES
('I23024312','PRG3204','PRG3204-AUG2024-8G1','PRG3204-AUG2024-8G2','no','PENDING')

INSERT INTO request_drop_course
(sid,cid,reason,status) VALUES
('I23024312','PRG3201','no','PENDING')

