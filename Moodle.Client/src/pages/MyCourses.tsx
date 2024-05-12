import { useEffect, useState } from "react";
import axios from "axios";
import Modal from "react-modal";
import { Navbar } from "../components/Navbar";
import { Footer } from "../components/Footer";
import "../styles/Home.css";
import WebSocketService from "../services/WebSocketService";

interface Course {
  id: number;
  name: string;
  code: string;
  credit: number;
}

interface Event {
    id: number;
    name: string;
    description: string;
}

export const MyCourses = () => {
  const userId = localStorage.getItem("userId");
  const roleId = localStorage.getItem("roleId");
  const [courses, setCourses] = useState<Course[]>([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [eventsModalIsOpen, setEventsModalIsOpen] = useState(false);
  const [eventName, setEventName] = useState("");
  const [eventDescription, setEventDescription] = useState("");
  const [selectedCourseId, setSelectedCourseId] = useState<number | null>(null); 
  const [events, setEvents] = useState<Event[]>([]);

  useEffect(() => {
    const fetchCourses = async () => {
      try {
        if (userId) {
          const response = await axios.get(
            `http://localhost:5191/api/User/GetCoursesByUser/${userId}`
          );
          if (response.data && response.data.success) {
            setCourses(response.data.data);
          }
        }
      } catch (error) {
        console.error("Error fetching courses:", error);
      }
    };

    fetchCourses();
  }, [userId]);

  const handleCreateEvent = async () => {
    if (!selectedCourseId || !eventName || !eventDescription) {
      console.error("Missing data");
      return;
    }

    try {
      const eventInfo = {
        courseId: selectedCourseId,
        name: eventName,
        description: eventDescription,
      };

      const response = await axios.post(
        "http://localhost:5191/api/Event/CreateEvent",
        eventInfo
      );
      if (response.data && response.data.success) {
        closeModal();
        alert("Event created successfully");
        const courseName = response.data.data.course.name;
        const courseCode = response.data.data.course.code;
        const eventName = response.data.data.name;
        WebSocketService.getInstance().sendMessage(
          `A new event has been created for the course '${courseCode}' ${courseName} with name ${eventName}`
        );
      }
    } catch (error) {
      console.error("Error creating event:", error);
    }
  };

  const GetEventsByCourseId = async (courseId: number) => {
    try {
      axios.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${localStorage.getItem("token")}`;
      const response = await axios.get(
        `http://localhost:5191/api/Event/GetEventsByCourseId/${courseId}`
      );
      if (response.data && response.data.success) {
        setEvents(response.data.data);
        setSelectedCourseId(courseId);
        setEventsModalIsOpen(true);
        console.log(response.data);
      }
    } catch (error) {
      console.error("Error while listing events: ", error);
    }
    };

  const openModal = (courseId: number) => {
    setSelectedCourseId(courseId);
    setModalIsOpen(true);
  };

  const closeModal = () => {
    setModalIsOpen(false);
    setSelectedCourseId(null);
    };

    const closeEventsModal = () => {
        setEventsModalIsOpen(false); 
        setSelectedCourseId(null);
    };

  return (
    <div>
      <Navbar />
      <h2>My Courses</h2>
      <div className="course-container">
        {courses.map((course) => (
          <div key={course.id} className="course">
            <div className="course-name">{course.name}</div>
            <div>
              {course.code}, Credits: {course.credit}
            </div>
                <button onClick={() => GetEventsByCourseId(course.id)}>View Events</button> 
                {roleId === "2" && (
                    <button onClick={() => openModal(course.id)}>Create Event</button>
            )}
          </div>
        ))}
      </div>
      //<Footer />

      <Modal
        isOpen={modalIsOpen}
        onRequestClose={closeModal}
        contentLabel="Create Event Modal"
      >
        <h2>Create Event</h2>
        <div className="modalInput">
          <input
            type="text"
            placeholder="Name"
            value={eventName}
            onChange={(e) => setEventName(e.target.value)}
          />
        </div>
        <br></br>
        <div className="modalInput">
          <input
            type="text"
            placeholder="Description"
            value={eventDescription}
            onChange={(e) => setEventDescription(e.target.value)}
          />
        </div>
        <br></br>
        <button onClick={handleCreateEvent}>Create</button>
        <button onClick={closeModal}>Cancel</button>
          </Modal>

          <Modal
              isOpen={eventsModalIsOpen} 
              onRequestClose={closeEventsModal} 
              contentLabel="Events List Modal"
          >
              <h2>Events List</h2>
              <div>
                  {events.map((event, index) => (
                      <div key={index}>
                          <h3>{event.name}</h3>
                          <p>{event.description}</p>
                      </div>
                  ))}
              </div>
              <button onClick={closeEventsModal}>Close</button>
          </Modal>




    </div>
  );
};

export default MyCourses;
