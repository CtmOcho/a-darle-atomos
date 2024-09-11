import React from 'react';

function HomePage() {
  return (
    <div className="">


      <section id="section-2" className="section section-2" style={{ backgroundImage: "url(/assets/atomos-destilacion.jpeg)" }}>
        <div className="content">
          <h1 style={{ color: "#ffffff" }}>A darle Átomos</h1>
          <h3 style={{ color: "#ffffff" }}>
            Buscamos promover el aprendizaje de la química en jovenes de chile a través de la experimentación y la tecnología.
          </h3>
        </div>
      </section>

      {/* Tercera sección */}
      <section id="section-3" className="section section-3">
        <div className="content">
          <h2>Experiencias que encontrarás</h2>
          <div className="experiencias">
            <div className="experiencia">
              <h3>Laboratorio virtual</h3>
              <p>Crea compuestos, utiliza implementos de medida, simula reacciones y más</p>
            </div>
            <div className="experiencia">
              <h3>Evaluaciones</h3>
              <p>Prueba tus conocimientos con actividadas y quizes</p>
            </div>
            <div className="experiencia">
              <h3>Panel de progreso profesores</h3>
              <p>Controla el progreso de tus estudiantes desde la comodidad del navegador</p>
            </div>
          </div>
        </div>
      </section>

      {/* Cuarta sección */}
      <section id="section-4" className="section section-4">
        <div className="content">
          <h2>Echa un vistazo a nuestras últimas novedades</h2>
          <div className="social-media">
            <div className="facebook">Contenido de Facebook</div>
            <div className="instagram">Contenido de Instagram</div>
          </div>
        </div>
      </section>
    </div>
  );
}

export default HomePage;
