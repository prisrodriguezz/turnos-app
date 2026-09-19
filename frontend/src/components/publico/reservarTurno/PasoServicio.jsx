import { useState } from "react";

import "../../../estilos/publico/reservarTurno/pasoServicio.css";

function PasoServicio({ onSeleccionar }) {
  const [servicioSeleccionado, setServicioSeleccionado] = useState(null);

  // Datos temporales.
  // Después los vamos a traer desde la API.

  const servicios = [
    {
      id: 1,
      nombre: "Limpieza facial",
      duracionMinutos: 60,
      costo: 15000,
    },
    {
      id: 2,
      nombre: "Manicura",
      duracionMinutos: 45,
      costo: 10000,
    },
    {
      id: 3,
      nombre: "Masaje relajante",
      duracionMinutos: 60,
      costo: 18000,
    },
    {
      id: 4,
      nombre: "Tratamiento facial",
      duracionMinutos: 90,
      costo: 22000,
    },
  ];

  const seleccionarServicio = (servicio) => {
    setServicioSeleccionado(servicio.id);
  };

  const continuar = () => {
    const servicio = servicios.find(
      (servicio) => servicio.id === servicioSeleccionado,
    );

    if (!servicio) return;

    onSeleccionar(servicio);
  };

  return (
    <div className="paso-servicio">
      <div className="paso-titulo">
        <span>Paso 1</span>

        <h2>¿Qué servicio querés realizarte?</h2>

        <p>Elegí una opción para comenzar con tu reserva.</p>
      </div>

      <div className="servicios-grid">
        {servicios.map((servicio) => (
          <button
            key={servicio.id}
            type="button"
            className={`servicio-card ${
              servicioSeleccionado === servicio.id ? "seleccionado" : ""
            }`}
            onClick={() => seleccionarServicio(servicio)}
          >
            <div className="servicio-check">
              {servicioSeleccionado === servicio.id ? "✓" : ""}
            </div>

            <div className="servicio-icono">✨</div>

            <h3>{servicio.nombre}</h3>

            <div className="servicio-info">
              <span>{servicio.duracionMinutos} minutos</span>

              <strong>${servicio.costo.toLocaleString("es-AR")}</strong>
            </div>
          </button>
        ))}
      </div>

      <div className="paso-acciones">
        <button
          type="button"
          className="btn btn--primary"
          disabled={!servicioSeleccionado}
          onClick={continuar}
        >
          Continuar
          <span>→</span>
        </button>
      </div>
    </div>
  );
}

export default PasoServicio;
