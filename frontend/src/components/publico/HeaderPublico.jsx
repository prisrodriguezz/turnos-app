import { useState } from "react";
import { Link, useLocation } from "react-router-dom";

import "../../estilos/publico/headerPublico.css";

function HeaderPublico() {
  const [menuAbierto, setMenuAbierto] = useState(false);

  const location = useLocation();

  const cerrarMenu = () => {
    setMenuAbierto(false);
  };

  const estaEnReserva = location.pathname === "/reservar";

  // HEADER PARA RESERVA
  if (estaEnReserva) {
    return (
      <header className="header-publico header-publico--reserva">
        <div className="header-contenido">

          <Link
            to="/"
            className="header-volver"
            onClick={cerrarMenu}
          >
            ← Volver al inicio
          </Link>

          <div className="logo">
            <Link to="/" onClick={cerrarMenu}>
              ✿
            </Link>
          </div>

        </div>
      </header>
    );
  }

  // HEADER NORMAL
  return (
    <header className="header-publico">
      <div className="header-contenido">

        {/* LOGO */}
        <div className="logo">
          <Link to="/" onClick={cerrarMenu}>
            ✿
          </Link>
        </div>

        {/* NAVEGACIÓN DESKTOP */}
        <nav className="nav-desktop">
          <a href="#hero" onClick={cerrarMenu}>
            Inicio
          </a>

          <a href="#servicios" onClick={cerrarMenu}>
            Servicios
          </a>

          <a href="#sobre-negocio" onClick={cerrarMenu}>
            Sobre Nosotros
          </a>
        </nav>

        {/* HAMBURGUESA */}
        <button
          type="button"
          className={`menu-btn ${menuAbierto ? "abierto" : ""}`}
          onClick={() => setMenuAbierto(!menuAbierto)}
          aria-label={
            menuAbierto ? "Cerrar menú" : "Abrir menú"
          }
          aria-expanded={menuAbierto}
        >
          <span></span>
          <span></span>
          <span></span>
        </button>

        {/* MENÚ MOBILE */}
        <nav
          className={`nav-mobile ${
            menuAbierto ? "abierta" : ""
          }`}
        >
          <a href="#hero" onClick={cerrarMenu}>
            Inicio
          </a>

          <a href="#servicios" onClick={cerrarMenu}>
            Servicios
          </a>

          <a href="#sobre-negocio" onClick={cerrarMenu}>
            Sobre Nosotros
          </a>
        </nav>

      </div>
    </header>
  );
}

export default HeaderPublico;