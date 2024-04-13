import { useEffect, useState } from "react";
import { ChangeEvent } from "react";
import axios from "axios";
import { Navbar } from "../components/Navbar";
import { Footer } from "../components/Footer";
import "../styles/Home.css";
import Modal from "react-modal";
import { Students } from "../components/Students";

Modal.setAppElement("#root");

interface Course {
  id: number;
  name: string;
  code: string;
  credit: number;
}

interface Degree {
  id: number;
  name: string;
}

interface Student {
  name: string;
  degree: Degree;
}

export const Home = () => {
  const [courses, setCourses] = useState<Course[]>([]);
  const [degrees, setDegrees] = useState<Degree[]>([]);
  const [filteredCourses, setFilteredCourses] = useState<Course[]>([]);
  const [courseNameFilter, setCourseNameFilter] = useState<string>("");
  const [degreeIdFilter, setDegreeIdFilter] = useState<number | null>(null);
  const [students, setStudents] = useState<Student[]>([]);
  const [errorMessage, setErrorMessage] = useState("");
  const [selectedCourseId, setSelectedCourseId] = useState<number | null>(null);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [selectedDegreeId, setSelectedDegreeId] = useState<number>(0);

  useEffect(() => {
    const fetchCourses = async () => {
      try {
        const coursesResponse = await axios.get(
          `http://localhost:5191/api/Course`
        );
        if (coursesResponse.data && coursesResponse.data.success) {
          setCourses(coursesResponse.data.data);
          setFilteredCourses(coursesResponse.data.data);
        }
        const degreesResponse = await axios.get(
          `http://localhost:5191/api/Degree/GetAll`
        );
        if (degreesResponse.data && degreesResponse.data.success) {
          setDegrees(degreesResponse.data.data);
          console.log(degreesResponse.data.data);
        }
      } catch (error) {
        console.error("Error fetching courses:", error);
      }
    };

    fetchCourses();
  }, []);

  const GetUserByCourseId = async (courseId: number) => {
    try {
      const response = await axios.get(
        `http://localhost:5191/api/User/GetUsersByCourseId/${courseId}`
      );
      if (response.data && response.data.success) {
        setStudents(response.data.data);
        setSelectedCourseId(courseId);
        setModalIsOpen(true);
        console.log(response.data);
      }
    } catch (error) {
      console.error("Error while listing students: ", error);
    }
  };

  const handleAddCourseToUser = async (courseId: number) => {
    const userId = localStorage.getItem("userId");
    if (!userId) {
      setErrorMessage("Nincs bejelentkezve felhasználó.");
      return;
    }

    try {
      const response = await axios.post(
        "http://localhost:5191/api/User/AddCourseToUser",
        {
          userId: userId,
          courseId: courseId,
        }
      );

      alert(response.data.message);
    } catch (error) {
      console.error("Error adding course to user:", error);
      setErrorMessage("Hiba történt a kurzus felvételekor.");
    }
  };

  const handleChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;

    if (name === "courseNameFilter") {
      setCourseNameFilter(value);
      const filtered = courses.filter((course) =>
        course.name.toLowerCase().includes(value.toLowerCase())
      );
      setFilteredCourses(filtered);
    } else if (name === "degreeIdFilter") {
      setDegreeIdFilter(Number(value));
      const filtered = courses.filter((course) =>
        course.name.toLowerCase().includes(value.toLowerCase())
      );
      setFilteredCourses(filtered);

      try {
        const response = await axios.get(
          `http://localhost:5191/api/Course/GetCoursesByDegree/${value}`
        );
        if (response.data && response.data.success) {
          setFilteredCourses(response.data.data);
          console.log(response.data.data);
        }
      } catch (error) {
        console.error("Error fetching courses by degree:", error);
      }
    }
  };

  const handleFilterChange = async (event: ChangeEvent<HTMLSelectElement>) => {
    const degreeId = Number(event.target.value);
    setSelectedDegreeId(degreeId);
    console.log(
      `Event.target.Value:${event.target.value} DegreeId: ${degreeId} SelectedDegreeId: ${selectedDegreeId}`
    );
    if (degreeId === 0) {
      try {
        setErrorMessage("");
        const coursesResponse = await axios.get(
          `http://localhost:5191/api/Course`
        );
        console.log(coursesResponse.data);
        if (coursesResponse.data && coursesResponse.data.success) {
          setFilteredCourses(coursesResponse.data.data);
        }
      } catch (error) {
        console.error("Error fetching courses:", error);
      }
    } else {
      fetchFilteredCourses(degreeId);
    }
  };

  const fetchFilteredCourses = async (degreeId: number) => {
    try {
      const response = await axios.get(
        `http://localhost:5191/api/Course/GetCoursesByDegree/${degreeId}`
      );
      if (response.data.data && response.data.success) {
        setFilteredCourses(response.data.data);
        setErrorMessage("");
        console.log(response.data.data);
      } else {
        setFilteredCourses([]);
        setErrorMessage(response.data.message);
      }
    } catch (error) {
      console.error("Error enrolling:", error);
    }
  };

  return (
    <div>
      <Navbar />
      <h2>Courses</h2>
      <div id="filterDiv">


        <div className="degree-filter-select">
            <label htmlFor="filter-select" style={{ fontWeight: "bold" }}>
             Filter by Degree 
             </label>
          <select
            id="filter-select"
            onChange={handleFilterChange}
            value={selectedDegreeId}
          >
            <option value="0">Válassz szakot</option>
            {degrees.map((degree) => (
              <option key={degree.id} value={degree.id}>
                {degree.name}
              </option>
            ))}
          </select>
        </div>
        <div className="course-filter">
          <label htmlFor="courseNameFilter" style={{ fontWeight: "bold" }}>
            Filter by Course
          </label>
          <input
            type="text"
            id="courseNameFilter"
            className="filter"
            name="courseNameFilter"
            value={courseNameFilter}
            onChange={handleChange}
            placeholder="Enter course name"
          />
        </div>
      </div>
      <div className="course-container">
        {errorMessage ? (
          <div className="error-message">{errorMessage}</div>
        ) : (
          filteredCourses.map((course) => (
            <div key={course.id} className="course">
              <div className="course-name">{course.name}</div>
              <div>
                <button onClick={() => GetUserByCourseId(course.id)}>
                  Hallgatók
                </button>
                <button onClick={() => handleAddCourseToUser(course.id)}>
                  Felvétel
                </button>
              </div>
            </div>
          ))
        )}
      </div>
      <Modal
        isOpen={modalIsOpen}
        onRequestClose={() => setModalIsOpen(false)}
        contentLabel="Hallgatók"
        ariaHideApp={false}
      >
        <h2>Hallgatók</h2>
        <Students students={students} />
      </Modal>
      {errorMessage && <div>{errorMessage}</div>}
      <Footer />
    </div>
  );
};
