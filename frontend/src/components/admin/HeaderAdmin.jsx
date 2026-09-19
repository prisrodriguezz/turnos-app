import { Link } from "react-router-dom";

function HeaderAdmin() {
  return (
    <header>
      <h2>Panel Administrativo</h2>

      <nav>
        <Link to="/admin">Dashboard</Link>

        <Link to="/admin/turnos">Turnos</Link>

        <Link to="/admin/servicios">Servicios</Link>

        <Link to="/admin/clientes">Clientes</Link>
      </nav>
    </header>
  );
}

export default HeaderAdmin;
