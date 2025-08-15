import Image from "next/image";
import Logo from "./components/Logo";

export default async function Home() {
  return (
    <div className="flex justify-center">
      <Logo size="large" />
    </div>
  );
}
