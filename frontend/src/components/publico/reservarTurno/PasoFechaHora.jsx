import { useState } from "react";

import "../../../estilos/publico/reservarTurno/pasoFechaHora.css";

function PasoFechaHora({
  servicio,
  profesional,
  onSeleccionar,
  onVolver,
}) {
  const [fechaSeleccionada, setFechaSeleccionada] = useState("");
  const [horarioSeleccionado, setHorarioSeleccionado] =
    useState(null);

  /*
   * MOCK
   *
   * Después los horarios vendrán
   * desde la API.
   */

  const horariosMock = [
    "09:00",
    "10:00",
    "11:00",
    "14:00",
    "15:00",
    "16:00",
    "17:00",
  ];

  /*
   * En el futuro:
   *
   * Los horarios dependerán de:
   * - Servicio seleccionado.
   * - Profesional seleccionado.
   * - Fecha seleccionada.
   *
   * La API devolverá únicamente
   * los horarios disponibles.
   */

  const seleccionarFecha = (event) => {
    setFechaSeleccionada(event.target.value);

    // Si cambia la fecha, se reinicia el horario.
    setHorarioSeleccionado(null);
  };

  const seleccionarHorario = (horario) => {
    setHorarioSeleccionado(horario);
  };

  const continuar = () => {
    if (!fechaSeleccionada || !horarioSeleccionado) {
      return;
    }

    onSeleccionar({
      fecha: fechaSeleccionada,
      horario: horarioSeleccionado,
    });
  };

  return (
    <div className="paso-fecha-hora">
      {/* TÍTULO */}

      <div className="paso-titulo">
        <span>Paso 3</span>

        <h2>¿Cuándo querés venir?</h2>

        <p>
          Elegí una fecha y un horario disponible para tu turno.
        </p>
      </div>

      {/* SERVICIO */}

      {servicio && (
        <div className="servicio-seleccionado">
          <span>Servicio elegido</span>

          <strong>{servicio.nombre}</strong>
        </div>
      )}

      {/* PROFESIONAL */}

      <div className="fecha-profesional">
        <span>Profesional</span>

        <strong>
          {profesional
            ? `${profesional.nombre} ${profesional.apellido}`
            : "Cualquiera"}
        </strong>
      </div>

      {/* FECHA */}

      <div className="fecha-selector">
        <label htmlFor="fecha">
          Seleccioná una fecha
        </label>

        <input
          type="date"
          id="fecha"
          value={fechaSeleccionada}
          min={new Date().toISOString().split("T")[0]}
          onChange={seleccionarFecha}
        />
      </div>

      {/* HORARIOS */}

      {fechaSeleccionada && (
        <div className="horarios-selector">
          <h3>Horarios disponibles</h3>

          <div className="horarios-grid">
            {horariosMock.map((horario) => (
              <button
                key={horario}
                type="button"
                className={`horario-card ${
                  horarioSeleccionado === horario
                    ? "seleccionado"
                    : ""
                }`}
                onClick={() => seleccionarHorario(horario)}
              >
                {horario}
              </button>
            ))}
          </div>
        </div>
      )}

      {/* ACCIONES */}

      <div className="paso-acciones">
        <button
          type="button"
          className="btn btn--secondary"
          onClick={onVolver}
        >
          ← Volver
        </button>

        <button
          type="button"
          className="btn btn--primary"
          disabled={
            !fechaSeleccionada || !horarioSeleccionado
          }
          onClick={continuar}
        >
          Continuar
          <span>→</span>
        </button>
      </div>
    </div>
  );
}

export default PasoFechaHora;