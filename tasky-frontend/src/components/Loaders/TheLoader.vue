<template>
  <div role="status" class="flex flex-col items-center">
    <!-- Dekoracja: czytnik ekranu ma przeczytać tylko napis poniżej -->
    <div class="loader-stage" aria-hidden="true">
      <!-- Scena A: odhaczanie zadania -->
      <div class="scene scene-a flex flex-col justify-center gap-2.5 px-3.5">
        <div
          class="row-a flex items-center gap-3.25 rounded-[15px] border border-line-warm bg-cream-50 px-3.75 py-3.5 shadow-soft"
        >
          <span
            class="checkbox flex size-6.5 flex-none items-center justify-center rounded-lg border-[1.5px]"
          >
            <svg
              class="text-white"
              width="15"
              height="15"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              stroke-width="3.4"
              stroke-linecap="round"
              stroke-linejoin="round"
            >
              <polyline class="tick" points="20 6 9 17 4 12" />
            </svg>
          </span>
          <span class="relative text-[15px] font-semibold text-ink">
            Finish the design review
            <span
              class="strike absolute inset-x-0 top-1/2 h-0.5 origin-left rounded-sm bg-ink-300"
            />
          </span>
        </div>
        <div class="flex items-center gap-3.25 px-3.75 opacity-50">
          <span class="size-6.5 flex-none rounded-lg border-[1.5px] border-line-strong" />
          <span class="text-[15px] font-semibold text-ink-400">Send the weekly update</span>
        </div>
      </div>

      <!-- Scena B: przeniesienie karty między kolumnami -->
      <div class="scene scene-b flex items-center justify-center gap-(--gap)">
        <div class="w-(--col)">
          <div class="mb-2.25 flex items-center gap-1.75 pl-0.5">
            <span class="size-2 rounded-full bg-accent-terracotta" />
            <span class="text-[11.5px] font-bold text-ink-600">To do</span>
          </div>
          <!-- Karta leży w kolumnie "To do", więc start zawsze pokrywa się ze slotem -->
          <div class="relative">
            <div class="slot h-14 rounded-xl border-[1.5px] border-dashed border-line-strong" />
            <div class="card-mover absolute inset-0 z-10">
              <div class="card h-full rounded-xl border border-line bg-cream-50 px-3 py-2.75">
                <div class="mb-2 h-1.25 w-4.5 rounded-full bg-accent-amber" />
                <div class="text-[12.5px] font-semibold leading-[1.35] text-ink">
                  Ship the release notes
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="w-(--col)">
          <div class="mb-2.25 flex items-center gap-1.75 pl-0.5">
            <span class="size-2 rounded-full bg-accent-green" />
            <span class="text-[11.5px] font-bold text-ink-600">Done</span>
          </div>
          <div
            class="slot slot-to h-14 rounded-xl border-[1.5px] border-dashed border-line-strong"
          />
        </div>
      </div>
    </div>

    <div class="mt-8.5 flex items-center gap-2.25">
      <span class="font-display text-[16px] font-bold tracking-[0.01em] text-ink">{{ label }}</span>
      <span class="flex gap-1" aria-hidden="true">
        <span class="dot size-1.25 rounded-full bg-coral" />
        <span class="dot size-1.25 rounded-full bg-coral" />
        <span class="dot size-1.25 rounded-full bg-coral" />
      </span>
    </div>
  </div>
</template>

<script setup lang="ts">
  withDefaults(defineProps<{ label?: string }>(), { label: 'Getting things in order' })
</script>

<style scoped>
  .loader-stage {
    --loop: 2s;
    --col: 126px;
    --gap: 14px;
    --shadow-lift: 0 12px 24px rgba(180, 120, 90, 0.28);

    position: relative;
    width: 300px;
    height: 150px;
  }

  .scene {
    position: absolute;
    inset: 0;
  }
  .scene-a {
    animation: sceneA var(--loop) ease-in-out infinite;
  }
  .scene-b {
    animation: sceneB var(--loop) ease-in-out infinite;
  }

  /* Scena A */
  .row-a {
    animation: rowFade var(--loop) ease-in-out infinite;
  }
  .checkbox {
    background: var(--color-cream-50);
    border-color: var(--color-line-strong);
    animation: boxFill var(--loop) ease-in-out infinite;
  }
  .tick {
    stroke-dasharray: 22;
    stroke-dashoffset: 22;
    animation: tickDraw var(--loop) ease-in-out infinite;
  }
  /* scaleX zamiast width: animacja bez przeliczania layoutu */
  .strike {
    transform: scaleX(0);
    animation: rowStrike var(--loop) ease-in-out infinite;
  }

  /* Scena B */
  .card-mover {
    animation: cardMove var(--loop) cubic-bezier(0.6, 0.05, 0.3, 1) infinite;
  }
  .card {
    animation: cardLift var(--loop) ease-in-out infinite;
  }
  .slot-to {
    animation: slotGlow var(--loop) ease-in-out infinite;
  }

  /* Kropki */
  .dot {
    animation: dots 1.2s ease-in-out infinite;
  }
  .dot:nth-child(2) {
    animation-delay: 0.2s;
  }
  .dot:nth-child(3) {
    animation-delay: 0.4s;
  }

  @keyframes sceneA {
    0% {
      opacity: 0;
      transform: scale(0.96);
    }
    4% {
      opacity: 1;
      transform: scale(1);
    }
    46% {
      opacity: 1;
      transform: scale(1);
    }
    50%,
    100% {
      opacity: 0;
      transform: scale(0.96);
    }
  }
  @keyframes sceneB {
    0%,
    50% {
      opacity: 0;
      transform: scale(0.96);
    }
    54%,
    96% {
      opacity: 1;
      transform: scale(1);
    }
    100% {
      opacity: 0;
      transform: scale(0.96);
    }
  }
  @keyframes boxFill {
    0%,
    8% {
      background: var(--color-cream-50);
      border-color: var(--color-line-strong);
    }
    16%,
    100% {
      background: var(--color-coral);
      border-color: var(--color-coral);
    }
  }
  @keyframes tickDraw {
    0%,
    10% {
      stroke-dashoffset: 22;
    }
    22%,
    100% {
      stroke-dashoffset: 0;
    }
  }
  @keyframes rowFade {
    0%,
    12% {
      opacity: 1;
    }
    22%,
    100% {
      opacity: 0.45;
    }
  }
  @keyframes rowStrike {
    0%,
    14% {
      transform: scaleX(0);
    }
    26%,
    100% {
      transform: scaleX(1);
    }
  }
  @keyframes cardMove {
    0%,
    54% {
      transform: translateX(0);
    }
    72%,
    100% {
      transform: translateX(calc(var(--col) + var(--gap)));
    }
  }
  @keyframes cardLift {
    0%,
    56% {
      box-shadow: var(--shadow-soft);
      transform: scale(1);
    }
    64%,
    74% {
      box-shadow: var(--shadow-lift);
      transform: scale(1.05);
    }
    82%,
    100% {
      box-shadow: var(--shadow-soft);
      transform: scale(1);
    }
  }
  @keyframes slotGlow {
    0%,
    74% {
      border-color: var(--color-line-strong);
      background: transparent;
    }
    82%,
    96% {
      border-color: var(--color-accent-green);
      background: color-mix(in srgb, var(--color-accent-green) 8%, transparent);
    }
    100% {
      border-color: var(--color-line-strong);
      background: transparent;
    }
  }
  @keyframes dots {
    0%,
    100% {
      opacity: 0.25;
    }
    50% {
      opacity: 1;
    }
  }

  @media (prefers-reduced-motion: reduce) {
    .scene-a,
    .row-a,
    .checkbox,
    .tick,
    .strike,
    .dot {
      animation: none;
    }
    .scene-b {
      display: none;
    }
    .dot {
      opacity: 0.6;
    }
  }
</style>
