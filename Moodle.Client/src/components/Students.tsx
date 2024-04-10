interface Student {
    name: string;
    degree: {
      name: string;
    };
  }
  
  interface StudentsProps {
    students: Student[];
  }
  
  export const Students: React.FC<StudentsProps> = ({ students }) => {
    return (
      <div>
        {students.map((student, index) => (
          <div key={index}>{student.name}</div>
        ))}
      </div>
    );
  };
  