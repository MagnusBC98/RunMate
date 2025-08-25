import Image from "next/image";

type LogoProps = {
  size?: "small" | "large";
};

export default function Logo({ size = "large" }: LogoProps) {
  let width = 800;
  let height = 300;

  if (size === "small") {
    width = 100;
    height = 33;
  }

  return (
    <Image
      src="/logo.jpg"
      alt="RunMate Logo"
      width={width}
      height={height}
      priority
    />
  );
}
