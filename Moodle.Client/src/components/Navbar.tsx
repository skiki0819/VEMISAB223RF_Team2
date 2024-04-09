import { Link } from "react-router-dom";


export const Navbar = () => {
  return (
    <div className='Navbar'>
      <div className="NavLinks">
        <Link to="/home"> Home </Link>
        <Link to="/myCourses"> My Courses </Link>
      </div>
      <div className="LogoutButton">
        <Link to="/"> Log out </Link>
      </div>
    </div>
  )
}
