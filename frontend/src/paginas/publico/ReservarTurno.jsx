import { useState } from "react";
import ResumenTurno from "../../components/publico/reservarTurno/ResumenTurno";
import PasoServicio from "../../components/publico/reservarTurno/PasoServicio";
import PasoProfesional from "../../components/publico/reservarTurno/PasoProfesional";
import PasoFechaHora from "../../components/publico/reservarTurno/PasoFechaHora";
import PasoDatos from "../../components/publico/reservarTurno/PasoDatos";
import PasoConfirmacion from "../../components/publico/reservarTurno/PasoConfirmacion";

import "../../estilos/publico/reservarTurno/reservarTurno.css";

function ReservarTurno() {
  /*
   * =========================================
   * CONFIGURACIÓN DEL NEGOCIO
   * =========================================
   */

  const configuracionNegocio = {
    usaProfesionales: true,
  };

  const seleccionarServicio = (servicio) => {
    setReserva((prev) => ({
      ...prev,
      servicio: servicio,
      profesional: null,
      fecha: null,
      horario: null,
    }));

    if (configuracionNegocio.usaProfesionales) {
      irAlPaso("profesional");
    } else {
      irAlPaso("fecha");
    }
  };

  const seleccionarProfesional = (profesional) => {
    setReserva((prev) => ({
      ...prev,

      profesional: profesional,

      fecha: null,
      horario: null,
    }));

    irAlPaso("fecha");
  };

  const seleccionarFechaHora = ({ fecha, horario }) => {
    setReserva((prev) => ({
      ...prev,
      fecha: fecha,
      horario: horario,
    }));

    irAlPaso("datos");
  };

  const guardarDatos = (cliente) => {
    setReserva((prev) => ({
      ...prev,
      cliente,
    }));

    irAlPaso("confirmacion");
  };

  /*
   * =========================================
   * PASOS
   * =========================================
   */

  const pasos = [
    {
      id: "servicio",
      nombre: "Servicio",
    },

    ...(configuracionNegocio.usaProfesionales
      ? [
          {
            id: "profesional",
            nombre: "Profesional",
          },
        ]
      : []),

    {
      id: "fecha",
      nombre: "Fecha y hora",
    },

    {
      id: "datos",
      nombre: "Tus datos",
    },

    {
      id: "confirmacion",
      nombre: "Confirmación",
    },
  ];

  /*
   * =========================================
   * ESTADO DEL PASO
   * =========================================
   */

  const [paso, setPaso] = useState(1);

  /*
   * =========================================
   * PASO ACTUAL
   * =========================================
   */

  const pasoActual = pasos[paso - 1];

  const irAlPaso = (idPaso) => {
    const indicePaso = pasos.findIndex((paso) => paso.id === idPaso);

    if (indicePaso === -1) return;

    setPaso(indicePaso + 1);

    //Actualizar scroll
    window.scrollTo({
      top: 0,
      behavior: "auto",
    });
  };

  /*
   * =========================================
   * RESERVA
   * =========================================
   */

  const [reserva, setReserva] = useState({
    servicio: null,
    profesional: null,
    fecha: null,
    horario: null,

    cliente: {
      nombre: "",
      apellido: "",
      telefono: "",
      email: "",
    },
  });

  /*
   * =========================================
   * NAVEGACIÓN
   * =========================================
   */

  const volverPaso = () => {
    if (paso <= 1) return;

    const pasoAnterior = pasos[paso - 2];

    irAlPaso(pasoAnterior.id);
  };

  return (
    <main className="reservar">
      {/*ENCABEZADO */}
      <section className="reservar-header">
        <span className="reservar-etiqueta">✨ Tu momento empieza acá</span>

        <h1>Reservá tu turno</h1>

        <p>
          Elegí el servicio que querés realizarte y encontrá el momento perfecto
          para vos.
        </p>
      </section>

      {/*PROGRESO */}
      <div className="reservar-progreso">
        {pasos.map((item, index) => {
          const numeroPaso = index + 1;

          return (
            <div key={item.id} className="progreso-item">
              <div
                className={`progreso-paso ${
                  paso >= numeroPaso ? "activo" : ""
                }`}
              >
                <span>{numeroPaso}</span>

                <p>{item.nombre}</p>
              </div>

              {index < pasos.length - 1 && (
                <div
                  className={`progreso-linea ${
                    paso > numeroPaso ? "activa" : ""
                  }`}
                />
              )}
            </div>
          );
        })}
      </div>

      {/* =====================================
                CONTENIDO
            ====================================== */}

      <section
        className={`reservar-contenido ${
          pasoActual.id === "confirmacion"
            ? "reservar-contenido--confirmacion"
            : ""
        }`}
      >
        {/* =================================
                    PANEL PRINCIPAL
                ================================== */}

        <div className="reservar-principal">
          {pasoActual.id === "servicio" && (
            <PasoServicio onSeleccionar={seleccionarServicio} />
          )}

          {pasoActual.id === "profesional" && (
            <PasoProfesional
              servicio={reserva.servicio}
              onSeleccionar={seleccionarProfesional}
              onVolver={volverPaso}
            />
          )}

          {pasoActual.id === "fecha" && (
            <PasoFechaHora
              servicio={reserva.servicio}
              profesional={reserva.profesional}
              onSeleccionar={seleccionarFechaHora}
              onVolver={volverPaso}
            />
          )}

          {pasoActual.id === "datos" && (
            <PasoDatos
              cliente={reserva.cliente}
              onGuardar={guardarDatos}
              onVolver={volverPaso}
            />
          )}

          {pasoActual.id === "confirmacion" && (
            <PasoConfirmacion
              reserva={reserva}
              onConfirmar={() => {
                console.log("Reserva a confirmar:", reserva);
              }}
              onVolver={volverPaso}
            />
          )}
        </div>

        {/* =================================
                    RESUMEN
                ================================== */}

        {pasoActual.id !== "confirmacion" && (
          <ResumenTurno reserva={reserva} />
        )}
        
      </section>
    </main>
  );
}

export default ReservarTurno;
