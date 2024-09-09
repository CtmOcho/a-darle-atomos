import React from 'react';

function HomePage() {
  return (
    <div className="">


      <section id="section-2" className="section section-2">
        <div className="content">
          <h2>¿Qué Ofrecemos?</h2>
          <p>
            Ofrecemos promover la empatía y concientización sobre las situaciones que experimentan las personas...
          </p>
        </div>
      </section>

      {/* Tercera sección */}
      <section id="section-3" className="section section-3">
        <div className="content">
          <h2>Experiencias que encontrarás</h2>
          <div className="experiencias">
            <div className="experiencia">
              <h3>Silla de Ruedas</h3>
              <p>Ponte en el lugar de alguien en silla de ruedas...</p>
            </div>
            <div className="experiencia">
              <h3>Embarazo</h3>
              <p>Experimenta cómo es vivir el embarazo...</p>
            </div>
            <div className="experiencia">
              <h3>Vejez</h3>
              <p>Camina como una persona de la tercera edad...</p>
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
