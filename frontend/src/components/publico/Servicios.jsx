import { Link } from "react-router-dom";
import ServicioCard from "../publico/ServicioCard";

import "../../estilos/publico/servicios.css";

function Servicios({servicios}) {
  return (
    <section id="servicios" className="servicios">
      <div className="servicios-header">
        <span>Nuestros servicios</span>

        <h2>Tratamientos pensados para tu bienestar</h2>

        <p>
          Elegí el servicio que mejor se adapte a vos y reservá tu turno online
          en pocos minutos.
        </p>
      </div>

      <div className="servicios-grid">
        {servicios.map((servicio) => (
          <ServicioCard key={servicio.id} {...servicio} />
        ))}
      </div>

      <Link to="/reservar" className="btn btn--primary">
        Reservar turno
      </Link>
    </section>
  );
}

export default Servicios;
