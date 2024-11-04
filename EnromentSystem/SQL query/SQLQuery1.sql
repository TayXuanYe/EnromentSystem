USE EnrolmentSystemDatabase
INSERT INTO student 
(sid,name,password,program,student_email,ic_or_passport,mode_of_study,school,level,major,scholarship) VALUES 
('I23024312','Tay Xuan Ye','iu040804130465','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)','i23024312@student.newiniti.edu.my','040804130465','Full Time','GBL110 INFORMATION TECHNOLOGY','DEGREE','SOFTWARE ENGINEERING',60)

INSERT INTO lecture
(lid,name,password) values
('L1','SB','SBSBBSBSB')

/*PRG3201*/
INSERT INTO course
(cid,name,credit_hours,available,price) VALUES 
('PRG3201','OBJECT ORIENTED PROGRAMMING',4,1,2555)
INSERT INTO course_major
(cid,major,program) VALUES
('PRG3201','NONE','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)')
INSERT INTO course_major
(cid,major,program) VALUES
('PRG3201','NONE','BITI')


INSERT INTO section
(sid,name,cid,semester,lid,program,max_enroll,current_enroll) VALUES
('PRG3201-AUG2024-8G1','8G1','PRG3201','AUG2024','L1','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)',30,0)
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8G1-1','PRG3201-AUG2024-8G1',40,'C1','lectural')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8G1-2','PRG3201-AUG2024-8G1',41,'C1','lectural')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8G1-3','PRG3201-AUG2024-8G1',8,'C1','lab')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8G1-4','PRG3201-AUG2024-8G1',9,'C1','lab')


INSERT INTO section
(sid,name,cid,semester,lid,program,max_enroll,current_enroll) VALUES
('PRG3201-AUG2024-8G2','8G2','PRG3201','AUG2024','L1','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)',30,0)
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8G2-1','PRG3201-AUG2024-8G2',42,'C1','lectural')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8G2-2','PRG3201-AUG2024-8G2',43,'C1','lectural')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8G2-3','PRG3201-AUG2024-8G2',7,'C1','lab')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8G2-4','PRG3201-AUG2024-8G2',6,'C1','lab')


INSERT INTO section
(sid,name,cid,semester,lid,program,max_enroll,current_enroll) VALUES
('PRG3201-AUG2024-8H1','8H1','PRG3201','AUG2024','L1','BITI',30,0)
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8H1-1','PRG3201-AUG2024-8H1',42,'C1','lectural')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8H1-2','PRG3201-AUG2024-8H1',43,'C1','lectural')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8H1-3','PRG3201-AUG2024-8H1',7,'C1','lab')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3201-AUG2024-8H1-4','PRG3201-AUG2024-8H1',6,'C1','lab')

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
(sid,name,cid,semester,lid,program,max_enroll,current_enroll) VALUES
('PRG3204-AUG2024-8G1','8G1','PRG3204','AUG2024','L1','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)',30,0)
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3204-AUG2024-8G1-1','PRG3204-AUG2024-8G1',42,'C1','lectural')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3204-AUG2024-8G1-2','PRG3204-AUG2024-8G1',43,'C1','lectural')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3204-AUG2024-8G1-3','PRG3204-AUG2024-8G1',7,'C1','lab')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3204-AUG2024-8G1-4','PRG3204-AUG2024-8G1',6,'C1','lab')

/*PRG3205*/
INSERT INTO course
(cid,name,credit_hours,available,price) VALUES 
('PRG3205','ALGORITHM',4,1,2555)
INSERT INTO course_major
(cid,major,program) VALUES
('PRG3205','SOFTWARE ENGINEERING','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)')
INSERT INTO section
(sid,name,cid,semester,lid,program,max_enroll,current_enroll) VALUES
('PRG3205-AUG2024-8G1','8G1','PRG3205','AUG2024','L1','BCSI - BACHELER OF COMPUTER SCIENCES (HONS)',30,0)
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3205-AUG2024-8G1-1','PRG3205-AUG2024-8G1',42,'C1','lectural')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3205-AUG2024-8G1-2','PRG3205-AUG2024-8G1',43,'C1','lectural')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3205-AUG2024-8G1-3','PRG3205-AUG2024-8G1',7,'C1','lab')
INSERT INTO class
(id,sid,time,class_room,type) VALUES
('PRG3205-AUG2024-8G1-4','PRG3205-AUG2024-8G1',6,'C1','lab')

INSERT INTO previous_compulsory_course
(sid,cid) VALUES 
('I23024312','PRG3201')

INSERT INTO student_taken_course
(sid,cid,section_id,status) VALUES
('I23024312','PRG3201','PRG3201-AUG2024-8G2','FAIL')

UPDATE request_add_course SET status = 'PENDING'

SELECT * FROM course WHERE cid NOT IN (SELECT cid FROM request_add_course )