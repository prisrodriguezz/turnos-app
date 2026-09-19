import "../../../estilos/publico/reservarTurno/pasoConfirmacion.css";

function PasoConfirmacion({ reserva, onConfirmar, onVolver }) {
  return (
    <div className="paso-confirmacion">
      <div className="paso-titulo">
        <span>Paso 5</span>

        <h2>Revisá los datos de tu turno</h2>

        <p>
          Verificá que toda la información sea correcta antes de confirmar.
        </p>
      </div>

        <div className="confirmacion-secciones">

            {/* ================================
                DATOS DEL TURNO
            ================================= */}

            <div className="confirmacion-seccion">
                <h3>Datos del turno</h3>

                <div className="confirmacion-datos">
                <div className="confirmacion-item">
                    <span>Servicio</span>
                    <strong>{reserva.servicio?.nombre}</strong>
                </div>

                <div className="confirmacion-item">
                    <span>Profesional</span>
                    <strong>
                    {reserva.profesional
                        ? `${reserva.profesional.nombre} ${reserva.profesional.apellido}`
                        : "Cualquiera"}
                    </strong>
                </div>

                <div className="confirmacion-item">
                    <span>Fecha</span>
                    <strong>{reserva.fecha}</strong>
                </div>

                <div className="confirmacion-item">
                    <span>Horario</span>
                    <strong>{reserva.horario} hs</strong>
                </div>
                </div>
            </div>

            {/* ================================
                DATOS DEL CLIENTE
            ================================= */}

            <div className="confirmacion-seccion">
                <h3>Tus datos</h3>

                <div className="confirmacion-datos">
                <div className="confirmacion-item">
                    <span>Nombre completo</span>
                    <strong>
                    {reserva.cliente.nombre} {reserva.cliente.apellido}
                    </strong>
                </div>

                <div className="confirmacion-item">
                    <span>Teléfono</span>
                    <strong>{reserva.cliente.telefono}</strong>
                </div>

                <div className="confirmacion-item">
                    <span>Email</span>
                    <strong>{reserva.cliente.email}</strong>
                </div>
                </div>
            </div>
        </div>

      {/* ================================
          RESUMEN ECONÓMICO
      ================================= */}

      <div className="confirmacion-total">
        <div>
          <span>Duración</span>
          <strong>{reserva.servicio?.duracionMinutos} minutos</strong>
        </div>

        <div>
          <span>Total</span>
          <strong>
            ${reserva.servicio?.costo.toLocaleString("es-AR")}
          </strong>
        </div>
      </div>

      {/* ================================
          ACCIONES
      ================================= */}

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
          onClick={onConfirmar}
        >
          Confirmar turno
          <span>✓</span>
        </button>
      </div>
    </div>
  );
}

export default PasoConfirmacion;