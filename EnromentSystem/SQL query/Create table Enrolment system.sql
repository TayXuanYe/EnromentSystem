USE EnrolmentSystemDatabase
DROP TABLE payment;
DROP TABLE student_take_attendance;
DROP TABLE lecturer_create_attendance_record;
DROP TABLE bank;
DROP TABLE student_enrol_successful;
DROP TABLE system_function_available;
DROP TABLE request_drop_course;
DROP TABLE request_add_course;
DROP TABLE request_change_section;
DROP TABLE student_taken_course;
DROP TABLE previous_compulsory_course;
DROP TABLE current_semester;
DROP TABLE class;
DROP TABLE section;
DROP TABLE major;
DROP TABLE program;
DROP TABLE school;
DROP TABLE course_prerequisite;
DROP TABLE course_major;
DROP TABLE course;
DROP TABLE student;
DROP TABLE admin;
DROP TABLE lecture;
DROP TABLE hop;


CREATE TABLE student(
	sid varchar(255) primary key,
	ic_or_passport varchar(255),
	name varchar(255),
	date_of_birth date,
	password varchar(255),
	mode_of_study varchar(255),
	school varchar(255),
	level varchar(255),
	program varchar(255),
	major varchar(255),
	scholarship float,
	permanent_address varchar(255),
	permanent_postcode varchar(255),
	permanent_city varchar(255),
	permanent_state varchar(255),
	permanent_country varchar(255),
	current_address varchar(255),
	current_postcode int,
	current_city varchar(255),
	current_state varchar(255),
	current_country varchar(255),
	primary_email varchar(255),
	alternative_email varchar(255),
	student_email varchar(255),
	tel_no bigint,
	hp_no varchar(20),
	emergency_contact_relationship varchar(255),
	emergency_contact_person varchar(255),
	emergency_contact_number varchar(255),
	bank_name varchar(255),
	bank_account varchar(255),
	bank_holder_name varchar(255),
	bank_verification_document varchar(255),
 	admission_date date
)

CREATE TABLE current_semester(
	semester varchar(255),
	credit_hour int
)

INSERT INTO current_semester(semester,credit_hour) VALUES ('AUG2024',20)

CREATE TABLE course(
  cid varchar(255) PRIMARY KEY,
  name varchar(255),
  credit_hours int,
  available bit,
  price float
)

CREATE TABLE course_prerequisite(
	cid VARCHAR(255),
	prerequisite varchar(255),
	PRIMARY KEY (cid,prerequisite),
	FOREIGN KEY (cid) REFERENCES course(cid)
)

CREATE TABLE course_major(
	cid VARCHAR(255),
	major varchar(255),
	program varchar(255),
	PRIMARY KEY (cid,major,program),
	FOREIGN KEY (cid) REFERENCES course(cid)
)
CREATE TABLE school(
	school varchar(255) PRIMARY KEY
);
CREATE TABLE program(
	program varchar(255) PRIMARY KEY,
	school varchar(255),
	level varchar(255),
	FOREIGN KEY (school) REFERENCES school(school)
);
CREATE TABLE major(
	major varchar(255) ,
	program varchar(255) ,
	PRIMARY KEY (major,program),
	FOREIGN KEY (program) REFERENCES program(program)
);
CREATE TABLE lecture(
	lid varchar(255) PRIMARY KEY,
	name varchar(255),
	password varchar(255)
)
CREATE TABLE section(
	sid varchar(255) PRIMARY KEY,
	name varchar(255),
	cid varchar(255),
	semester varchar(255),
	program varchar(255),
	max_enroll int,
	current_enroll INT DEFAULT 0,
	FOREIGN KEY (cid) REFERENCES course(cid),
)

CREATE TABLE class(
	id varchar(255) PRIMARY KEY,
	sid varchar(255),
	time int,
	class_room VARCHAR(255),
	lid varchar(255),
	type varchar(255),
	FOREIGN KEY (lid) REFERENCES lecture(lid),
	FOREIGN KEY (sid) REFERENCES section(sid)
)

CREATE TABLE previous_compulsory_course(
	sid VARCHAR(255),
	cid VARCHAR(255),
	PRIMARY KEY (sid,cid)
)

CREATE TABLE student_taken_course(
	sid varchar(255),
	cid varchar(255),
	section_id varchar(255),
	status varchar(255),
	PRIMARY KEY (sid,cid),
	FOREIGN KEY (sid) REFERENCES student(sid),
	FOREIGN KEY (cid) REFERENCES course(cid),
	FOREIGN KEY (section_id) REFERENCES section(sid)
);
-- status {'FAIL','ADD','TAKEN','COMPLETE'}

-- STATUS {'HOLD','PENDING','APPROVE','NOT APPROVE'}
CREATE TABLE request_drop_course(
	rid int IDENTITY(1,1) PRIMARY KEY,
	sid varchar(255),
	cid varchar(255),
	reason varchar(1000),
	status varchar(255),
	create_time DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (sid) REFERENCES student(sid),
	FOREIGN KEY (cid) REFERENCES course(cid),
);
CREATE TABLE request_add_course(
	rid int IDENTITY(1,1) PRIMARY KEY,
	sid varchar(255),
	cid varchar(255),
	section_id varchar(255),
	reason varchar(1000),
	status varchar(255),
	create_time DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (sid) REFERENCES student(sid),
	FOREIGN KEY (cid) REFERENCES course(cid),
	FOREIGN KEY (section_id) REFERENCES section(sid)
);
CREATE TABLE request_change_section(
	rid int IDENTITY(1,1) PRIMARY KEY,
	sid varchar(255),
	cid varchar(255),
	current_section_id varchar(255),
	target_section_id varchar(255),
	reason varchar(1000),
	status varchar(255),
	create_time DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (sid) REFERENCES student(sid),
	FOREIGN KEY (cid) REFERENCES course(cid),
	FOREIGN KEY (current_section_id) REFERENCES section(sid),
	FOREIGN KEY (target_section_id) REFERENCES section(sid)
);

CREATE TABLE student_enrol_successful(
	sid varchar(255) PRIMARY KEY
);

CREATE TABLE system_function_available(
	system_function varchar(255) PRIMARY KEY,
	available bit
);

INSERT INTO system_function_available VALUES('ENROL','1')
INSERT INTO system_function_available VALUES('ADDDROP','1')

CREATE TABLE admin(
	aid varchar(255) primary key,
	name varchar(255),
	password varchar(255)
);

INSERT INTO admin VALUES ('A23024312','Admin 1','admin');

CREATE TABLE bank(
	bank_name varchar(255)
);

INSERT INTO bank (bank_name) VALUES
    ('Malayan Banking Berhad'),
    ('CIMB Bank Berhad'),
    ('Public Bank Berhad'),
    ('RHB Bank Berhad'),
    ('Hong Leong Bank Berhad'),
    ('AmBank (M) Berhad'),
    ('Bank Islam Malaysia Berhad'),
    ('Alliance Bank Malaysia Berhad'),
    ('OCBC Bank (Malaysia) Berhad'),
    ('HSBC Bank Malaysia Berhad'),
    ('Standard Chartered Bank Malaysia Berhad'),
    ('United Overseas Bank (Malaysia) Berhad'),
    ('Bank Muamalat Malaysia Berhad'),
    ('Affin Bank Berhad'),
    ('Bank Pertanian Malaysia Berhad');

CREATE TABLE hop(
	hid varchar(255) primary key,
	fullname varchar(255),
	password varchar(255),
	email varchar(255)
);


CREATE TABLE lecturer_create_attendance_record(
	rid varchar(255) PRIMARY KEY,
	classId varchar(255),
	courseId varchar(255),
	sectionId varchar(255),
	date varchar(255),
	FOREIGN KEY (classId) REFERENCES class(id)
);

CREATE TABLE student_take_attendance(
	rid varchar(255) PRIMARY KEY,
	sid varchar(255),
	FOREIGN KEY (sid) REFERENCES student(sid),
	FOREIGN KEY (rid) REFERENCES lecturer_create_attendance_record(rid)
);

CREATE TABLE payment(
	pid int PRIMARY KEY IDENTITY(1, 1) ,
	sid varchar(255),
	date date DEFAULT GETDATE(),
	process varchar(255),
	particulars varchar(255),
	documentNo varchar(255),
	session varchar(255),
	amount float,
	FOREIGN KEY (sid) REFERENCES student(sid)
);
