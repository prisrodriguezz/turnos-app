import "../../../estilos/publico/reservarTurno/pasoProfesional.css";

function PasoProfesional({ servicio, onSeleccionar, onVolver }) {
  /*
   * MOCK
   *
   * Después estos profesionales
   * vendrán desde la API.
   */

  const profesionales = [
    {
      id: 1,
      nombre: "María",
      apellido: "González",
    },
    {
      id: 2,
      nombre: "Laura",
      apellido: "Fernández",
    },
    {
      id: 3,
      nombre: "Carla",
      apellido: "Rodríguez",
    },
  ];

  /*
   * Por ahora no necesitamos guardar
   * un estado local.
   *
   * Cuando el usuario hace click,
   * informamos al componente padre.
   */

  const seleccionar = (profesional) => {
    onSeleccionar(profesional);
  };

  return (
    <div className="paso-profesional">
      {/* =================================
                TÍTULO
            ================================== */}

      <div className="paso-titulo">
        <span>Paso 2</span>

        <h2>¿Con quién querés realizarte el servicio?</h2>

        <p>
          Elegí a tu profesional o dejá que nosotros encontremos una opción
          disponible.
        </p>
      </div>

      {/* =================================
                SERVICIO SELECCIONADO
            ================================== */}

      {servicio && (
        <div className="servicio-seleccionado">
          <span>Servicio elegido</span>

          <strong>{servicio.nombre}</strong>
        </div>
      )}

      {/* =================================
                CUALQUIERA
            ================================== */}

      <button
        type="button"
        className="profesional-card profesional-cualquiera"
        onClick={() => seleccionar(null)}
      >
        <div className="profesional-avatar">✨</div>

        <div className="profesional-info">
          <h3>Cualquiera</h3>

          <p>Mostrame los horarios disponibles de cualquier profesional.</p>
        </div>

        <span className="profesional-flecha">→</span>
      </button>

      {/* =================================
                PROFESIONALES
            ================================== */}

      <div className="profesionales-lista">
        {profesionales.map((profesional) => (
          <button
            key={profesional.id}
            type="button"
            className="profesional-card"
            onClick={() => seleccionar(profesional)}
          >
            <div className="profesional-avatar">
              {profesional.nombre.charAt(0)}
            </div>

            <div className="profesional-info">
              <h3>
                {profesional.nombre} {profesional.apellido}
              </h3>

              <p>Especialista</p>
            </div>

            <span className="profesional-flecha">→</span>
          </button>
        ))}
      </div>

        {/* BOTON VOLVER */}
      <div className="paso-acciones">
        <button
          type="button"
          className="btn btn--secondary btn-volver"
          onClick={onVolver}
        >
          ← Volver
        </button>
      </div>
    </div>
  );
}

export default PasoProfesional;
