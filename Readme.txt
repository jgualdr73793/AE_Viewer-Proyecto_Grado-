<div align="center">

# 🛡️ Virtual Twin for Military Training
**AI & Photogrammetry Pipeline**

*Proyecto desarrollado para el Ejército Nacional de Colombia*

![Aplicación Funcional](URL_FOTO_APP_FUNCIONAL_AQUI)

[![Python](https://img.shields.io/badge/Python-3776AB?style=for-the-badge&logo=python&logoColor=white)]()
[![TensorFlow](https://img.shields.io/badge/TensorFlow-FF6F00?style=for-the-badge&logo=tensorflow&logoColor=white)]()
[![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)]()
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)]()
[![Blender](https://img.shields.io/badge/Blender-F5792A?style=for-the-badge&logo=blender&logoColor=white)]()

</div>

---

> **Resumen Ejecutivo:** Aplicación móvil interactiva para el entrenamiento táctico en identificación de Artefactos Explosivos (AE) y Artefactos Explosivos Improvisados (AEI). Mediante un pipeline que integra segmentación semántica (IA) y fotogrametría, el sistema permite reducir riesgos operativos y estandarizar los protocolos de reconocimiento mediante *Gemelos Virtuales* de alta fidelidad.

---

## ⚙️ Arquitectura y Pipeline Técnico (End-to-End)

El sistema fue construido mediante un pipeline de 4 fases que conecta Inteligencia Artificial con renderizado 3D en tiempo real:

### 1. Recolección de Datos y Segmentación (U-Net)
* Se construyó un dataset de **+500 imágenes** tomadas en campo.
* Se entrenó un modelo de segmentación semántica basado en la arquitectura **U-Net** para crear máscaras binarias.
* **🎯 Resultado:** Exactitud del **96.2%**, aislando eficientemente el artefacto de fondos complejos.

### 2. Reconstrucción 3D (Fotogrametría Computacional)
* Utilizando las imágenes pre-segmentadas, se usó **Meshroom** para generar nubes de puntos densas.
* Se aprovechó la aceleración por hardware (**CUDA**) para capturar detalles anatómicos críticos.
* **🎯 Resultado:** Reconstrucciones con un error dimensional medio de apenas **0.89 mm**.

<div align="center">
  <img src="URL_FOTO_MESHROOM_AQUI" alt="Reconstrucción Meshroom" width="70%">
  <p><em>Nube de puntos densa y reconstrucción inicial utilizando Meshroom.</em></p>
</div>

### 3. Optimización Topológica
* Las mallas 3D crudas fueron importadas a **Blender** para un proceso exhaustivo de limpieza (*Retopology*).
* **🎯 Resultado:** Reducción del **60% en el conteo de polígonos**, optimizando el modelo para móviles sin sacrificar detalles.

<div align="center">
  <img src="URL_FOTO_BLENDER_AQUI" alt="Limpieza en Blender" width="70%">
  <p><em>Proceso de optimización de polígonos y limpieza de malla en Blender.</em></p>
</div>

### 4. Despliegue Interactivo (Unity)
* Los modelos optimizados (`.fbx`) se integraron en el motor gráfico de **Unity**.
* Se desarrollaron scripts en **C#** para controlar la interacción táctil (rotación, zoom, selección).
* **🎯 Resultado:** La aplicación corre de manera estable a **58 FPS** en dispositivos Android.

<div align="center">
  <img src="URL_FOTO_UNITY_AQUI" alt="Modelos en Unity" width="70%">
  <p><em>Interfaz y jerarquía de objetos configurados desde el editor de Unity.</em></p>
</div>

---

## 💻 Instalación y Configuración Local

### 🛠️ Prerrequisitos
* **Python 3.8** * **Git** y **Visual Studio Code** * *Opcional pero recomendado:* **CUDA 11.1.1** y **cuDNN 9.7.8** (Para aceleración GPU de NVIDIA).

### 🚀 Despliegue del Entorno (Windows)

Abre una terminal (`Git Bash` o la integrada en `VS Code`) y ejecuta:

```powershell
# 1. Clonar el repositorio
git clone <AQUÍ_EL_LINK_DE_TU_REPOSITORIO>

# 2. Instalar herramienta de entornos virtuales
pip install virtualenv

# 3. Crear y aislar el entorno (Python 3.8)
virtualenv -p python3.8 env
cd env

# 4. Dar permisos y activar (PowerShell)
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process
./Scripts/activate

# 5. Instalar dependencias
pip install -r "../Ruta_archivo_requerimientos.txt"
