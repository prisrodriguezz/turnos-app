import { useState } from "react";
import "../../../estilos/publico/reservarTurno/pasoDatos.css";

function PasoDatos({ cliente, onGuardar, onVolver }) {
  const [datos, setDatos] = useState(cliente);

  const [errores, setErrores] = useState({});

  const cambiarCampo = (event) => {
    const { name, value } = event.target;

    setDatos((prev) => ({
      ...prev,
      [name]: value,
    }));

    // Limpiar el error del campo mientras el usuario escribe
    if (errores[name]) {
      setErrores((prev) => ({
        ...prev,
        [name]: "",
      }));
    }
  };

  const validar = () => {
    const nuevosErrores = {};

    if (!datos.nombre.trim()) {
      nuevosErrores.nombre = "Ingresá tu nombre.";
    }

    if (!datos.apellido.trim()) {
      nuevosErrores.apellido = "Ingresá tu apellido.";
    }

    if (!datos.telefono.trim()) {
      nuevosErrores.telefono = "Ingresá tu teléfono.";
    }else{
        const telefonoLimpio = datos.telefono.replace(/\D/g, "");

        if (telefonoLimpio.length < 10 || telefonoLimpio.length > 13) {
            nuevosErrores.telefono = "Ingresá un número de teléfono válido.";
        }
    }

    if (!datos.email.trim()) {
      nuevosErrores.email = "Ingresá tu email.";
    } else if (!/\S+@\S+\.\S+/.test(datos.email)) {
      nuevosErrores.email = "Ingresá un email válido.";
    }

    setErrores(nuevosErrores);

    return Object.keys(nuevosErrores).length === 0;
  };

  const continuar = () => {
    if (!validar()) return;

    onGuardar(datos);
  };

  return (
    <div className="paso-datos">
      <div className="paso-titulo">
        <span>Paso 4</span>

        <h2>¿Cuáles son tus datos?</h2>

        <p>
          Necesitamos algunos datos para poder confirmar tu turno.
        </p>
      </div>

      <form className="datos-formulario" onSubmit={(event) => {
        event.preventDefault();
        continuar();
      }}>
        <div className="datos-grid">
          <div className="campo">
            <label htmlFor="nombre">
              Nombre
            </label>

            <input
              type="text"
              id="nombre"
              name="nombre"
              value={datos.nombre}
              onChange={cambiarCampo}
              placeholder="Ej. María"
              className={errores.nombre ? "campo-error" : ""}
            />

            {errores.nombre && (
              <span className="mensaje-error">
                {errores.nombre}
              </span>
            )}
          </div>

          <div className="campo">
            <label htmlFor="apellido">
              Apellido
            </label>

            <input
              type="text"
              id="apellido"
              name="apellido"
              value={datos.apellido}
              onChange={cambiarCampo}
              placeholder="Ej. González"
              className={errores.apellido ? "campo-error" : ""}
            />

            {errores.apellido && (
              <span className="mensaje-error">
                {errores.apellido}
              </span>
            )}
          </div>

          <div className="campo">
            <label htmlFor="telefono">
              Teléfono
            </label>

            <input
              type="tel"
              id="telefono"
              name="telefono"
              value={datos.telefono}
              onChange={cambiarCampo}
              placeholder="Ej. 379 1234567"
              className={errores.telefono ? "campo-error" : ""}
            />

            {errores.telefono && (
              <span className="mensaje-error">
                {errores.telefono}
              </span>
            )}
          </div>

          <div className="campo">
            <label htmlFor="email">
              Email
            </label>

            <input
              type="email"
              id="email"
              name="email"
              value={datos.email}
              onChange={cambiarCampo}
              placeholder="Ej. maria@email.com"
              className={errores.email ? "campo-error" : ""}
            />

            {errores.email && (
              <span className="mensaje-error">
                {errores.email}
              </span>
            )}
          </div>
        </div>

        <div className="paso-acciones">
          <button
            type="button"
            className="btn btn--secondary"
            onClick={onVolver}
          >
            ← Volver
          </button>

          <button
            type="submit"
            className="btn btn--primary"
          >
            Continuar
            <span>→</span>
          </button>
        </div>
      </form>
    </div>
  );
}

export default PasoDatos;